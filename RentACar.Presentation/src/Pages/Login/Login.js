import React, { useState } from "react";
import { Form, Input, Button, Checkbox, Row, Col, Alert, Card } from "antd";
import { UserOutlined, LockOutlined } from "@ant-design/icons";
import "./Login.css";
import { useAuth } from "../../Hooks/useAuth";
import { useHistory } from "react-router-dom";

function Login() {
    return (
        <Row className="fullHeight" type="flex" justify="center" align="middle">
            <Col>
                <LoginForm />
            </Col>
        </Row>
    );
}

function LoginForm() {
    const [message, setMessage] = useState(null);
    const [loading, setLoading] = useState(false);
    let auth = useAuth();
    let history = useHistory();

    const submit = (values) => {
        if (loading) {
            return;
        }

        setLoading(true);

        auth.signin(values.username, values.password, values.remember)
            .then((res) => history.replace("/home"))
            .catch((err) => {
                setMessage(err.message);
                setLoading(false);
            });
    };

    return (
        <>
            {message ? (
                <>
                    <Alert
                        message={message}
                        showIcon={false}
                        closable={true}
                        type="error"
                    />{" "}
                    <br />
                </>
            ) : null}
            <Card id="login-form">
                <Form
                    name="normal_login"
                    initialValues={{ remember: true }}
                    onFinish={submit}
                >
                    <Form.Item
                        name="username"
                        rules={[
                            {
                                required: true,
                                message: "Please input your Username!",
                            },
                        ]}
                    >
                        <Input
                            prefix={
                                <UserOutlined className="site-form-item-icon" />
                            }
                            placeholder="Username"
                        />
                    </Form.Item>
                    <Form.Item
                        name="password"
                        rules={[
                            {
                                required: true,
                                message: "Please input your Password!",
                            },
                        ]}
                    >
                        <Input
                            prefix={
                                <LockOutlined className="site-form-item-icon" />
                            }
                            type="password"
                            placeholder="Password"
                        />
                    </Form.Item>

                    <Form.Item>
                        <Form.Item
                            name="remember"
                            valuePropName="checked"
                            noStyle
                        >
                            <Checkbox>Remember me</Checkbox>
                        </Form.Item>
                    </Form.Item>

                    <Form.Item className="no-bottom-margin">
                        <Button
                            block
                            type="primary"
                            htmlType="submit"
                            className="login-form-button"
                        >
                            {loading ? "Singing In" : "Sing In"}
                        </Button>
                    </Form.Item>
                </Form>
            </Card>
        </>
    );
}

export default Login;
