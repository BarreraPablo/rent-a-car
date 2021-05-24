import { Button, Card, Col, Form, Input, Row } from "antd";
import React, { useState } from "react";
import { useHistory } from "react-router-dom";
import * as userService from "../../services/userService";
import "./Register.css";

function Register() {
    return (
        <Row className="fullHeight" type="flex" justify="center" align="middle">
            <Col>
                <RegisterForm />
            </Col>
        </Row>
    );
}

function RegisterForm() {
    const [loading, setLoading] = useState(false);
    const [form] = Form.useForm();
    let history = useHistory();

    const submit = (values) => {
        console.log(values);

        setLoading(true);

        userService
            .register(values)
            .then(() => history.push("./login"))
            .catch((err) => {
                if (err) {
                    const keys = Object.keys(err);
                    keys.forEach((key) => {
                        form.setFields([
                            {
                                name: key,
                                errors: [err[key]],
                            },
                        ]);
                    });
                }
            })
            .finally(() => setLoading(false));
    };

    return (
        <>
            <Card id="register-form">
                <Form
                    initialValues={{ requiredMarkValue: "optional" }}
                    onFinish={submit}
                    layout="vertical"
                    form={form}
                    requiredMark="optional"
                >
                    <Form.Item
                        name="username"
                        label="Username"
                        rules={[
                            {
                                required: true,
                                message: "Please input your Username!",
                            },
                        ]}
                    >
                        <Input />
                    </Form.Item>
                    <Form.Item
                        name="emailAddress"
                        label="Email Address"
                        rules={[
                            {
                                type: "email",
                                message: "The input is not valid E-mail!",
                            },
                        ]}
                    >
                        <Input />
                    </Form.Item>
                    <Form.Item
                        name="password"
                        label="Password"
                        rules={[
                            {
                                required: true,
                                message: "Please input your password!",
                            },
                            {
                                max: 10,
                                message:
                                    "The password is too large (maximum is 10 characters)",
                            },
                            {
                                min: 6,
                                message:
                                    "The password is too short (minimum is 6 characters)",
                            },
                        ]}
                        hasFeedback
                    >
                        <Input.Password />
                    </Form.Item>

                    <Form.Item
                        name="confirm"
                        label="Confirm Password"
                        dependencies={["password"]}
                        hasFeedback
                        rules={[
                            {
                                required: true,
                                message: "Please confirm your password!",
                            },
                            ({ getFieldValue }) => ({
                                validator(_, value) {
                                    if (
                                        !value ||
                                        getFieldValue("password") === value
                                    ) {
                                        return Promise.resolve();
                                    }
                                    return Promise.reject(
                                        new Error(
                                            "The two passwords that you entered do not match!"
                                        )
                                    );
                                },
                            }),
                        ]}
                    >
                        <Input.Password />
                    </Form.Item>

                    <Form.Item className="no-bottom-margin">
                        <Button
                            block
                            type="primary"
                            htmlType="submit"
                            className="login-form-button"
                            loading={loading}
                        >
                            {loading ? "Creating Account" : "Create Account"}
                        </Button>
                    </Form.Item>
                </Form>
            </Card>
        </>
    );
}

export default Register;
