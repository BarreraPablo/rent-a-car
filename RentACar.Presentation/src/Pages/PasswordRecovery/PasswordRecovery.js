import { Button, Card, Col, Form, Input, Result, Row } from "antd";
import React, { useState } from "react";
import { Link } from "react-router-dom";
import * as userService from "../../services/userService";
import "./PasswordRecovery.css";

function PasswordRecovery() {
    return (
        <Row className="fullHeight" type="flex" justify="center" align="middle">
            <Col>
                <PasswordRecoveryForm />
            </Col>
        </Row>
    );
}

function PasswordRecoveryForm() {
    const [loading, setLoading] = useState(false);
    const [success, setSuccess] = useState(false);
    const [form] = Form.useForm();

    const submit = (values) => {
        console.log(values);

        setLoading(true);

        userService
            .startPasswordRecovery(values.emailAddress)
            .then(() => setSuccess(true))
            .finally(() => setLoading(false));
    };

    return (
        <>
            <Card id="register-form">
                {success ? (
                    <Result
                        title="Your request has been processed"
                        subTitle="If the email entered corresponds to a registered user you will receive an email"
                        extra={
                            <Link to="/login">
                                <Button type="primary" key="login">
                                    Go to login
                                </Button>
                            </Link>
                        }
                    />
                ) : null
                }
                <Form
                    onFinish={submit}
                    layout="vertical"
                    hidden={success}
                    form={form}
                >
                    <Form.Item
                        name="emailAddress"
                        label="Type the account's email address"
                        rules={[
                            {
                                type: "email",
                                message: "The input is not valid E-mail!",
                            },
                            {
                                required: true,
                                message: "Please input a email address!",
                            },
                        ]}
                    >
                        <Input />
                    </Form.Item>

                    <Form.Item className="no-bottom-margin">
                        <Button
                            block
                            type="primary"
                            htmlType="submit"
                            className="login-form-button"
                            loading={loading}
                        >
                            Recover password
                        </Button>
                    </Form.Item>
                </Form>
            </Card>
        </>
    );
}

export default PasswordRecovery;
