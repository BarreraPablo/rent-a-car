import { LockOutlined, UserOutlined } from "@ant-design/icons";
import { Alert, Button, Card, Col, Form, Input, Row } from "antd";
import Checkbox from "antd/lib/checkbox/Checkbox";
import React, { useState } from "react";
import { Link, useHistory } from "react-router-dom";
import { useAuth } from "../../Hooks/useAuth";
import "./Login.css";

function Login() {
    return (
        <Row className="fullHeight" type="flex" justify="center" align="middle">
            <Col style={{ marginTop: "-17vh" }}>
                <Row
                    type="flex"
                    justify="center"
                    align="middle"
                    style={{ marginBottom: "10px" }}
                >
                    <Col>
                        <img
                            src={process.env.PUBLIC_URL + "/rent-logo.png"}
                            height="200"
                            alt="Rent a car logo"
                        />
                    </Col>
                </Row>

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
        setLoading(true);

        auth.signin(values.username, values.password)
            .then((res) => history.replace("/cars"))
            .catch((err) => {
                setMessage(err.message);
                setLoading(false);
            });
    };

    return (
        <>
            {message ? (
                <>
                    <Alert message={message} showIcon={false} type="error" />
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
                        initialValue="Test"
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
                        initialValue="123456"
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
                        You do not have an account?{" "}
                        <Link to="./register">Register now</Link><br/>
                        Forgot password?       {" "}
                        <Link to="/passwordrecovery" classNname="login-form-forgot">Recover password</Link>
                    </Form.Item>
                    <Form.Item className="no-bottom-margin">
                        <Button
                            block
                            type="primary"
                            htmlType="submit"
                            className="login-form-button"
                            loading={loading}
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
