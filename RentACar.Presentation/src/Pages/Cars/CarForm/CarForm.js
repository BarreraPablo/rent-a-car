import { UploadOutlined } from "@ant-design/icons";
import {
    Button,
    Card,
    Col,
    Form,
    Image,
    Input,
    InputNumber,
    message,
    Result,
    Row,
    Select,
    Upload,
} from "antd";
import { Option } from "antd/lib/mentions";
import React, { useEffect, useState } from "react";
import { NavLink, useLocation, useParams } from "react-router-dom";
import * as bodyTypeService from "../../../services/bodyTypeService";
import * as brandService from "../../../services/brandService";
import * as CarService from "../../../services/carService";
import "./CarForm.css";
import { NEW, EDIT, SHOW } from "../constants";

export function CarForm() {
    const { id } = useParams();
    const [form] = Form.useForm();
    const location = useLocation();
    const [imageUrl, setImageUrl] = useState("");
    const [brands, setBrands] = useState([]);
    const [bodyTypes, setBodyTypes] = useState([]);
    const [action, setAction] = useState(NEW);
    const [loading, setLoading] = useState(false);
    const [showSuccess, setShowSuccess] = useState(false);

    useEffect(() => {
        brandService
            .getAll()
            .then((res) => {
                setBrands(res);
            })
            .catch((err) => message.error(err));
        bodyTypeService
            .getAll()
            .then((res) => setBodyTypes(res))
            .catch((err) => message.error(err));

        setFormAction();
    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    const setFormAction = () => {
        if (location.pathname.includes(SHOW)) {
            setAction(SHOW);
            fillData();
        } else if (location.pathname.includes(EDIT)) {
            setAction(EDIT);
            fillData();
        }
    };

    const fillData = () => {
        CarService.getById(id)
            .then((car) => {
                setImageUrl(car.image);

                form.setFieldsValue({
                    model: car.model,
                    brandId: car.brand.id,
                    doors: car.doors,
                    airConditioner: car.airConditioner,
                    pricePerDay: car.pricePerDay,
                    gearbox: car.gearbox,
                    year: car.year,
                    bodyTypeId: car.bodyType.id,
                    seats: car.seats,
                    available: car.available,
                });
            })
            .catch((err) => message.error(err));
    };

    const validateMessages = {
        required: "This field is required!",
    };

    const beforeUpload = (file) => {
        if (file.type !== "image/png") {
            message.error(`${file.name} is not a png file`);
        }
        return file.type === "image/png" ? false : Upload.LIST_IGNORE;
    };

    const normFile = (e) => {
        if (Array.isArray(e)) {
            return e;
        }
        return e && e.fileList;
    };

    const setEdit = () => setAction(EDIT);

    const onFinish = (values) => {
        setLoading(true);
        values.id = parseInt(id);
        values.image = values.image ? values.image[0].originFileObj : null;

        CarService.saveCar(values, action)
            .then((res) => {
                setShowSuccess(true);
            })
            .catch((err) => message.error(err))
            .finally(() => setLoading(false));
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
            className: "disabled-input",
        },
        select: {
            bordered: action !== SHOW,
            showArrow: action !== SHOW,
            open: action === SHOW ? false : undefined,
        },
    };

    return (
        <div className="site-card-border-less-wrapper">
            <Card title="Car information" bordered={false}>
                {showSuccess ? (
                    <Result
                        status="success"
                        title="Car Saved Successfully"
                        extra={[
                            <NavLink to="/cars">
                                <Button type="primary" key="console">
                                    Back to car list
                                </Button>
                            </NavLink>,
                            <NavLink to="/reservations">
                                <Button key="buy">Create reservation</Button>,
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
                                name="model"
                                label="Model"
                                rules={[{ required: true }]}
                            >
                                <Input {...actionProps.input} />
                            </Form.Item>
                            <Form.Item
                                name="brandId"
                                label="Brand"
                                rules={[{ required: true }]}
                            >
                                <Select {...actionProps.select}>
                                    {brands.map((brand) => (
                                        <Option value={brand.id}>
                                            {brand.name}
                                        </Option>
                                    ))}
                                </Select>
                            </Form.Item>
                            <Form.Item
                                name="doors"
                                label="Doors"
                                rules={[{ required: true }]}
                            >
                                <InputNumber
                                    {...actionProps.input}
                                    min={1}
                                    max={5}
                                />
                            </Form.Item>
                            <Form.Item
                                name="airConditioner"
                                label="Air conditioner"
                                defaultValue={false}
                                rules={[{ required: true }]}
                            >
                                <Select {...actionProps.select}>
                                    <Option value={true}>Yes</Option>
                                    <Option value={false}>No</Option>
                                </Select>
                            </Form.Item>
                            <Form.Item
                                name="pricePerDay"
                                label="Price per day"
                                rules={[{ required: true }]}
                            >
                                <InputNumber
                                    {...actionProps.input}
                                    min={400}
                                    max={5000}
                                />
                            </Form.Item>

                            <Form.Item
                                name="gearbox"
                                label="Gearbox"
                                defaultValue={false}
                                rules={[{ required: true }]}
                            >
                                <Select {...actionProps.select}>
                                    <Option value="Automatic">Automatic</Option>
                                    <Option value="Manual">Manual</Option>
                                </Select>
                            </Form.Item>

                            <Form.Item
                                name="year"
                                label="Year"
                                rules={[{ required: true }]}
                            >
                                <InputNumber
                                    {...actionProps.input}
                                    min={2000}
                                    max={2030}
                                />
                            </Form.Item>

                            <Form.Item
                                name="seats"
                                label="Seats"
                                rules={[{ required: true }]}
                            >
                                <InputNumber
                                    {...actionProps.input}
                                    min={2}
                                    max={15}
                                />
                            </Form.Item>

                            <Form.Item
                                name="bodyTypeId"
                                label="Body type"
                                rules={[{ required: true }]}
                            >
                                <Select {...actionProps.select}>
                                    {bodyTypes.map((bodyType) => (
                                        <Option value={bodyType.id}>
                                            {bodyType.name}
                                        </Option>
                                    ))}
                                </Select>
                            </Form.Item>

                            <Form.Item
                                hidden={action === NEW}
                                name="available"
                                label="Available"
                                defaultValue={false}
                                rules={[{ required: action === EDIT }]}
                            >
                                <Select
                                    className="disabled-input"
                                    {...actionProps.select}
                                >
                                    <Option value={true}>Yes</Option>
                                    <Option value={false}>No</Option>
                                </Select>
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
                                    hidden={action === SHOW}
                                    htmlType="submit"
                                    loading={loading}
                                >
                                    Save
                                </Button>
                            </Form.Item>
                        </Col>
                        <Col span={12}>
                            <Row type="flex" justify="center" align="middle">
                                <Col span={14}>
                                    <Image
                                        src={imageUrl}
                                        width="100%"
                                        fallback="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAMIAAADDCAYAAADQvc6UAAABRWlDQ1BJQ0MgUHJvZmlsZQAAKJFjYGASSSwoyGFhYGDIzSspCnJ3UoiIjFJgf8LAwSDCIMogwMCcmFxc4BgQ4ANUwgCjUcG3awyMIPqyLsis7PPOq3QdDFcvjV3jOD1boQVTPQrgSkktTgbSf4A4LbmgqISBgTEFyFYuLykAsTuAbJEioKOA7DkgdjqEvQHEToKwj4DVhAQ5A9k3gGyB5IxEoBmML4BsnSQk8XQkNtReEOBxcfXxUQg1Mjc0dyHgXNJBSWpFCYh2zi+oLMpMzyhRcASGUqqCZ16yno6CkYGRAQMDKMwhqj/fAIcloxgHQqxAjIHBEugw5sUIsSQpBobtQPdLciLEVJYzMPBHMDBsayhILEqEO4DxG0txmrERhM29nYGBddr//5/DGRjYNRkY/l7////39v///y4Dmn+LgeHANwDrkl1AuO+pmgAAADhlWElmTU0AKgAAAAgAAYdpAAQAAAABAAAAGgAAAAAAAqACAAQAAAABAAAAwqADAAQAAAABAAAAwwAAAAD9b/HnAAAHlklEQVR4Ae3dP3PTWBSGcbGzM6GCKqlIBRV0dHRJFarQ0eUT8LH4BnRU0NHR0UEFVdIlFRV7TzRksomPY8uykTk/zewQfKw/9znv4yvJynLv4uLiV2dBoDiBf4qP3/ARuCRABEFAoBEgghggQAQZQKAnYEaQBAQaASKIAQJEkAEEegJmBElAoBEgghggQAQZQKAnYEaQBAQaASKIAQJEkAEEegJmBElAoBEgghggQAQZQKAnYEaQBAQaASKIAQJEkAEEegJmBElAoBEgghggQAQZQKAnYEaQBAQaASKIAQJEkAEEegJmBElAoBEgghggQAQZQKAnYEaQBAQaASKIAQJEkAEEegJmBElAoBEgghggQAQZQKAnYEaQBAQaASKIAQJEkAEEegJmBElAoBEgghggQAQZQKAnYEaQBAQaASKIAQJEkAEEegJmBElAoBEgghggQAQZQKAnYEaQBAQaASKIAQJEkAEEegJmBElAoBEgghggQAQZQKAnYEaQBAQaASKIAQJEkAEEegJmBElAoBEgghggQAQZQKAnYEaQBAQaASKIAQJEkAEEegJmBElAoBEgghggQAQZQKAnYEaQBAQaASKIAQJEkAEEegJmBElAoBEgghggQAQZQKAnYEaQBAQaASKIAQJEkAEEegJmBElAoBEgghgg0Aj8i0JO4OzsrPv69Wv+hi2qPHr0qNvf39+iI97soRIh4f3z58/u7du3SXX7Xt7Z2enevHmzfQe+oSN2apSAPj09TSrb+XKI/f379+08+A0cNRE2ANkupk+ACNPvkSPcAAEibACyXUyfABGm3yNHuAECRNgAZLuYPgEirKlHu7u7XdyytGwHAd8jjNyng4OD7vnz51dbPT8/7z58+NB9+/bt6jU/TI+AGWHEnrx48eJ/EsSmHzx40L18+fLyzxF3ZVMjEyDCiEDjMYZZS5wiPXnyZFbJaxMhQIQRGzHvWR7XCyOCXsOmiDAi1HmPMMQjDpbpEiDCiL358eNHurW/5SnWdIBbXiDCiA38/Pnzrce2YyZ4//59F3ePLNMl4PbpiL2J0L979+7yDtHDhw8vtzzvdGnEXdvUigSIsCLAWavHp/+qM0BcXMd/q25n1vF57TYBp0a3mUzilePj4+7k5KSLb6gt6ydAhPUzXnoPR0dHl79WGTNCfBnn1uvSCJdegQhLI1vvCk+fPu2ePXt2tZOYEV6/fn31dz+shwAR1sP1cqvLntbEN9MxA9xcYjsxS1jWR4AIa2Ibzx0tc44fYX/16lV6NDFLXH+YL32jwiACRBiEbf5KcXoTIsQSpzXx4N28Ja4BQoK7rgXiydbHjx/P25TaQAJEGAguWy0+2Q8PD6/Ki4R8EVl+bzBOnZY95fq9rj9zAkTI2SxdidBHqG9+skdw43borCXO/ZcJdraPWdv22uIEiLA4q7nvvCug8WTqzQveOH26fodo7g6uFe/a17W3+nFBAkRYENRdb1vkkz1CH9cPsVy/jrhr27PqMYvENYNlHAIesRiBYwRy0V+8iXP8+/fvX11Mr7L7ECueb/r48eMqm7FuI2BGWDEG8cm+7G3NEOfmdcTQw4h9/55lhm7DekRYKQPZF2ArbXTAyu4kDYB2YxUzwg0gi/41ztHnfQG26HbGel/crVrm7tNY+/1btkOEAZ2M05r4FB7r9GbAIdxaZYrHdOsgJ/wCEQY0J74TmOKnbxxT9n3FgGGWWsVdowHtjt9Nnvf7yQM2aZU/TIAIAxrw6dOnAWtZZcoEnBpNuTuObWMEiLAx1HY0ZQJEmHJ3HNvGCBBhY6jtaMoEiJB0Z29vL6ls58vxPcO8/zfrdo5qvKO+d3Fx8Wu8zf1dW4p/cPzLly/dtv9Ts/EbcvGAHhHyfBIhZ6NSiIBTo0LNNtScABFyNiqFCBChULMNNSdAhJyNSiECRCjUbEPNCRAhZ6NSiAARCjXbUHMCRMjZqBQiQIRCzTbUnAARcjYqhQgQoVCzDTUnQIScjUohAkQo1GxDzQkQIWejUogAEQo121BzAkTI2agUIkCEQs021JwAEXI2KoUIEKFQsw01J0CEnI1KIQJEKNRsQ80JECFno1KIABEKNdtQcwJEyNmoFCJAhELNNtScABFyNiqFCBChULMNNSdAhJyNSiECRCjUbEPNCRAhZ6NSiAARCjXbUHMCRMjZqBQiQIRCzTbUnAARcjYqhQgQoVCzDTUnQIScjUohAkQo1GxDzQkQIWejUogAEQo121BzAkTI2agUIkCEQs021JwAEXI2KoUIEKFQsw01J0CEnI1KIQJEKNRsQ80JECFno1KIABEKNdtQcwJEyNmoFCJAhELNNtScABFyNiqFCBChULMNNSdAhJyNSiECRCjUbEPNCRAhZ6NSiAARCjXbUHMCRMjZqBQiQIRCzTbUnAARcjYqhQgQoVCzDTUnQIScjUohAkQo1GxDzQkQIWejUogAEQo121BzAkTI2agUIkCEQs021JwAEXI2KoUIEKFQsw01J0CEnI1KIQJEKNRsQ80JECFno1KIABEKNdtQcwJEyNmoFCJAhELNNtScABFyNiqFCBChULMNNSdAhJyNSiEC/wGgKKC4YMA4TAAAAABJRU5ErkJggg=="
                                    />
                                    <Form.Item
                                        name="image"
                                        label="Upload"
                                        hidden={action === SHOW}
                                        valuePropName="fileList"
                                        getValueFromEvent={normFile}
                                    >
                                        <Upload
                                            name="logo"
                                            listType="picture"
                                            beforeUpload={beforeUpload}
                                            maxCount={1}
                                        >
                                            <Button icon={<UploadOutlined />}>
                                                Click to upload (only PNG)
                                            </Button>
                                        </Upload>
                                    </Form.Item>
                                </Col>
                            </Row>
                        </Col>
                    </Row>
                </Form>
            </Card>
        </div>
    );
}
