import {
    AutoComplete,
    Button,
    Card,
    Col,
    DatePicker,
    Form,
    Input,
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
import * as documentTypeService from "../../../services/documentTypeService";
import * as clientService from "../../../services/clientService";
import * as countryService from "../../../services/countryService";
import { EDIT, NEW, SHOW } from "../constants";

function ClientForm() {
    const { id } = useParams();
    const [form] = Form.useForm();
    const location = useLocation();
    const [action, setAction] = useState(NEW);
    const [loading, setLoading] = useState(false);
    const [showSuccess, setShowSuccess] = useState(false);
    const [documentTypes, setDocumentsTypes] = useState([]);
    const [countries, setCountries] = useState([]);

    useEffect(() => {
        getFormData();
        setFormAction();
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    const validateMessages = {
        required: "This field is required!",
    };

    const layout = {
        labelCol: { span: 4 },
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
        },
    };

    const fillData = () => {
        clientService
            .getById(id)
            .then((res) => {
                console.log('res', res)
                form.setFieldsValue({
                    firstName: res.firstName,
                    lastName: res.lastName,
                    documentNumber: res.documentNumber,
                    street: res.street,
                    phoneNumber: res.phoneNumber,
                    emailAddress: res.emailAddress,
                    birthday: moment(res.birthday),
                    country: res.country ? res.country.name : "",
                    documentTypeId: res.documentType.id
                });
            })
            .catch((err) => message.error(err));
    };

    const getFormData = () => {
        documentTypeService
            .getAll()
            .then((res) => setDocumentsTypes(res))
            .catch((err) => message.error(err));

        countryService
            .getAll()
            .then((res) => {
                const countries = res.map(country => ({key: country.id, value: country.name}));
                setCountries(countries)
            })
            .catch((err) => message.error(err))
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
        console.log("values", values);
        setLoading(true);
        values.country = values.country.toLowerCase();
        const selectedCountry = countries.find(c => c.value.toLowerCase() === values.country); 

        console.log("selectedCountry", selectedCountry);

        const client = {
            ...values,
            id: id,
            birthday: values.birthday.format("YYYY-MM-DD"),
            country: selectedCountry ? {id: selectedCountry.key, name: selectedCountry.value } : {id: 0, name: values.country}
        };

        console.log("newvalues", client);

        clientService
            .save(client, action)
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
                        title="Client Saved Successfully"
                        extra={[
                            <NavLink to="/clients">
                                <Button type="primary" key="console">
                                    Back to clients list
                                </Button>
                            </NavLink>,
                            <NavLink to="/reservations/new">
                                <Button key="buy">
                                    Create new reservation
                                </Button>
                            </NavLink>,
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
                                name="firstName"
                                label="First Name"
                                rules={[{ required: true }]}
                            >
                                <Input {...actionProps.input} />
                            </Form.Item>
                            <Form.Item
                                name="lastName"
                                label="Last Name"
                                rules={[{ required: true }]}
                            >
                                <Input {...actionProps.input} />
                            </Form.Item>
                            <Form.Item
                                name="documentTypeId"
                                label="Document Type"
                                defaultValue={false}
                                rules={[{ required: true }]}
                            >
                                <Select {...actionProps.select}>
                                    {documentTypes.map((doc) => (
                                        <Option value={doc.id}>
                                            {doc.name}
                                        </Option>
                                    ))}
                                </Select>
                            </Form.Item>
                            <Form.Item
                                name="documentNumber"
                                label="Document Number"
                                rules={[{ required: true }]}
                            >
                                <Input {...actionProps.input} />
                            </Form.Item>
                            <Form.Item
                                name="country"
                                label="Country"
                                rules={[{ required: true }]}
                                >
                                <AutoComplete
                                    options={countries}
                                    {...actionProps.input}
                                    style={{
                                        width: 200,
                                    }}
                                    filterOption={(inputValue, option) =>
                                        option.value.toUpperCase().indexOf(inputValue.toUpperCase()) !== -1
                                      }
                                    placeholder="input here"
                                />
                            </Form.Item>
                            <Form.Item
                                name="street"
                                label="Street"
                                rules={[{ required: true }]}
                            >
                                <Input {...actionProps.input} />
                            </Form.Item>
                            <Form.Item
                                name="phoneNumber"
                                label="Phone Number"
                                rules={[{ required: true }]}
                            >
                                <Input {...actionProps.input} />
                            </Form.Item>
                            <Form.Item
                                name="emailAddress"
                                label="Email Address"
                                rules={[{ required: true, type: "email" }]}
                            >
                                <Input {...actionProps.input} />
                            </Form.Item>
                            <Form.Item
                                label="Birthday"
                                name="birthday"
                                rules={[{ required: true }]}
                            >
                                <DatePicker {...actionProps.input} />
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

export default ClientForm;
