import {
    Button,
    Card,
    Col,
    DatePicker,
    Form,
    message,
    Radio,
    Result,
    Row,
    Select,
} from "antd";
import { Option } from "antd/lib/mentions";
import moment from "moment";
import { useEffect, useState } from "react";
import { useLocation, useParams } from "react-router";
import { NavLink } from "react-router-dom";
import * as carService from "../../../services/carService";
import * as clientService from "../../../services/clientService";
import * as paymentTypeService from "../../../services/paymentTypeService";
import * as reservationService from "../../../services/reservationService";
import { EDIT, NEW, SHOW } from "../constants";

const { RangePicker } = DatePicker;

function ReservationForm() {
    const { id } = useParams();
    const [form] = Form.useForm();
    const location = useLocation();
    const [action, setAction] = useState(NEW);
    const [loading, setLoading] = useState(false);
    const [showSuccess, setShowSuccess] = useState(false);
    const [clients, setClients] = useState([]);
    const [cars, setCars] = useState([]);
    const [paymentTypes, setPaymentTypes] = useState([]);

    useEffect(() => {
        getFormData();
        setFormAction();
    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    const validateMessages = {
        required: "This field is required!",
    };

    const layout = {
        labelCol: { span: 3 },
        wrapperCol: { span: 16 },
    };

    const tailLayout = {
        wrapperCol: { offset: 4, span: 12 },
    };

    const actionProps = {
        input: {
            bordered: action !== SHOW,
            disabled: action === SHOW,
        },
        select: {
            bordered: action !== SHOW,
            disabled: action === SHOW,
        },
        rangePicker: {
            bordered: action !== SHOW,
            disabled: action === SHOW,
        }
    };

    const fillData = () => {
        reservationService
            .getById(id)
            .then((res) => {
                console.log("res", res);
                form.setFieldsValue({
                    clientId: res.client.id,
                    carId: res.car.id,
                    paymentTypeId: res.paymentType.id,
                    paid: res.status !== "Pending",
                    "range-picker": [moment(res.pickUp), moment(res.dropOff)],
                });
            })
            .catch((err) => message.error(err));
    };

    const getFormData = () => {
        clientService
            .getAll()
            .then((res) => {
                console.log("clients", res);
                setClients(res);
            })
            .catch((err) => message.error(err));

        carService
            .getCars(true)
            .then((res) => {
                setCars(res);
            })
            .catch((err) => message.error(err));

        paymentTypeService
            .getAll()
            .then((res) => setPaymentTypes(res))
            .catch((err) => message.error(err));
    };

    const setFormAction = () => {
        if (location.pathname.includes(SHOW)) {
            setAction(SHOW);
            fillData();
        } else if (location.pathname.includes(EDIT)) {
            setAction(EDIT);
            fillData();
        }
    };

    const disabledDate = (current) => {
        // Can not select days before today and today
        return current && current.valueOf() < Date.now();
    };

    const onFinish = (values) => {
        console.log("onFinish", values);
        const rangeValue = values["range-picker"];
        setLoading(true);

        const reservation = {
            ...values,
            id: id,
            pickUp: rangeValue[0].format("YYYY-MM-DD"),
            dropOff: rangeValue[1].format("YYYY-MM-DD"),
        };
        reservationService
            .save(reservation, action)
            .then((res) => setShowSuccess(true))
            .catch((err) => {
                if (typeof err === "string") {
                    message.error(err);
                } else {
                    const keys = Object.keys(err);
                    keys.forEach((key) => {
                        if (key === "bussinessValidation") {
                            message.error(err[key][0]);
                        } else {
                            form.setFields([
                                {
                                    name: key,
                                    errors: [err[key]],
                                },
                            ]);
                        }
                    });
                }
            })
            .finally(() => setLoading(false));
    };

    const setEdit = () => setAction(EDIT);

    const onReset = () => {
        setShowSuccess(false);
        form.resetFields();
    };

    const rangeConfig = {
        rules: [
            { type: "array", required: true, message: "Please select time!" },
        ],
    };

    return (
        <div className="site-card-border-less-wrapper">
            <Card title="Reservation" bordered={false}>
                {showSuccess ? (
                    <Result
                        status="success"
                        title="Reservation Saved Successfully"
                        extra={[
                            <NavLink to="/reservations">
                                <Button type="primary" key="console">
                                    Back to reservation list
                                </Button>
                            </NavLink>,
                            <Button key="buy" onClick={onReset}>
                                Create new reservation
                            </Button>,
                        ]}
                    />
                ) : null}

                <Form
                    {...layout}
                    form={form}
                    hidden={showSuccess}
                    name="control-hooks"
                    onFinish={onFinish}
                    requiredMark={false}
                    validateMessages={validateMessages}
                >
                    <Row>
                        <Col span={12}>
                            <Form.Item
                                name="clientId"
                                label="Client"
                                defaultValue={false}
                                rules={[{ required: true }]}
                            >
                                <Select
                                    showSearch
                                    optionFilterProp="children"
                                    {...actionProps.select}
                                >
                                    {clients.map((client) => (
                                        <Option value={client.id}>
                                            {client.documentNumber +
                                                " - " +
                                                client.lastName +
                                                ", " +
                                                client.firstName}
                                        </Option>
                                    ))}
                                </Select>
                            </Form.Item>
                            <Form.Item
                                name="carId"
                                label="Car"
                                defaultValue={false}
                                rules={[{ required: true }]}
                            >
                                <Select
                                    showSearch
                                    optionFilterProp="children"
                                    {...actionProps.select}
                                >
                                    {cars.map((car) => (
                                        <Option value={car.id}>
                                            {car.brand.name +
                                                " - " +
                                                car.model +
                                                " - Price per day $" +
                                                car.pricePerDay + 
                                                " Seats: " + car.seats + 
                                                " Air coinditioner: " + (car.airCoinditioner ? "Yes" : "No")}
                                        </Option>
                                    ))}
                                </Select>
                            </Form.Item>

                            <Form.Item
                                name="range-picker"
                                label="RangePicker"
                                {...rangeConfig}
                            >
                                <RangePicker
                                    {...actionProps.rangePicker}
                                    disabledDate={disabledDate}
                                />
                            </Form.Item>
                            <Form.Item
                                name="paymentTypeId"
                                label="Payment Type"
                                defaultValue={false}
                                rules={[{ required: true }]}
                            >
                                <Select {...actionProps.select}>
                                    {paymentTypes.map((paymentType) => (
                                        <Option value={paymentType.id}>
                                            {paymentType.name}
                                        </Option>
                                    ))}
                                </Select>
                            </Form.Item>

                            <Form.Item
                                name="paid"
                                label="Paid"
                                defaultValue={false}
                                rules={[{ required: true }]}
                            >
                                <Radio.Group {...actionProps.input}>
                                    <Radio value={true}>Yes</Radio>
                                    <Radio value={false}>No</Radio>
                                </Radio.Group>
                            </Form.Item>

                            <Form.Item {...tailLayout}>
                                <Button
                                    type="primary"
                                    size="large"
                                    block
                                    hidden={action !== SHOW}
                                    onClick={setEdit}
                                >
                                    Edit
                                </Button>
                                <Button
                                    type="primary"
                                    size="large"
                                    block
                                    hidden={action === "show"}
                                    htmlType="submit"
                                    loading={loading}
                                >
                                    Save
                                </Button>
                            </Form.Item>
                        </Col>
                        <Col span={12}></Col>
                    </Row>
                </Form>
            </Card>
        </div>
    );
}

export default ReservationForm;
