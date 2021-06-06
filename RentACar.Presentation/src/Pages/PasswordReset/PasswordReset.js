import { Button, Card, Col, Form, Input, message, Row } from "antd";
import React, { useState } from "react";
import { useHistory, useParams } from "react-router-dom";
import * as userService from "../../services/userService";
import "./PasswordReset.css";

function PasswordReset() {
    return (
        <Row className="fullHeight" type="flex" justify="center" align="middle">
            <Col>
                <PasswordResetForm />
            </Col>
        </Row>
    );
}

function PasswordResetForm() {
    const [loading, setLoading] = useState(false);
    const [form] = Form.useForm();
    const history = useHistory();
    const { recoveryToken } = useParams(); 

    const submit = (values) => {
        console.log(values);
        console.log('recovery token', recoveryToken);

        setLoading(true);

        userService
            .recoverPassword(values.password, recoveryToken)
            .then(() => {
                message.success('Password updated successfully')
                history.push("/login")
            })
            .catch((err) => {
                if (err && err.recoveryToken) {
                    message.error("You already reseted your password")
                    history.push("/login")
                }
                else if (err) {
                    const keys = Object.keys(err);
                    keys.forEach((key) => {
                        form.setFields([
                            {
                                name: key,
                                errors: [err[key]],
                            },
                        ]);
                    });
                } else {
                    history.push("/login");
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
                        name="password"
                        label="New Password"
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
                            loading={loading}
                        >
                            Reset password
                        </Button>
                    </Form.Item>
                </Form>
            </Card>
        </>
    );
}

export default PasswordReset;
