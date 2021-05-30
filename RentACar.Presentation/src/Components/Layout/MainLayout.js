import {
    CarOutlined, FileOutlined,


    SettingOutlined, TeamOutlined,
    UserOutlined
} from '@ant-design/icons';
import { Layout, Menu } from 'antd';
import { useState } from 'react';
import { NavLink } from "react-router-dom";
import { useAuth } from '../../Hooks/useAuth';
import './MainLayout.css';

const { Header, Content, Footer, Sider } = Layout;
const { SubMenu } = Menu;

function MainLayout({children}) {
    const [collapsed, setCollapsed] = useState(false);
    let auth = useAuth();

    const onCollapse = collapsed => {
        setCollapsed(collapsed);
    }

    return (
        <Layout style={{ minHeight: "100vh", backgroundColor: "red" }}>
            <Sider
                collapsible
                collapsed={collapsed}
                onCollapse={onCollapse}
            >
                    <div className="logo">
                        <img src={process.env.PUBLIC_URL +  "/rent-horizontal-logo-gris.png"} width="165" alt="Rent a car logo" />
                    </div>
                <Menu theme="dark" defaultSelectedKeys={["1"]} mode="inline">
                    <Menu.Item key="1" icon={<CarOutlined />}>
                        <NavLink to="/cars">
                            Cars
                        </NavLink>
                    </Menu.Item>
                    <Menu.Item key="2" icon={<FileOutlined />}>
                        <NavLink to="/reservations">
                            Reservations
                        </NavLink>
                    </Menu.Item>
                    <Menu.Item key="3" icon={<UserOutlined />}>
                        <NavLink to="/clients">
                            Clients
                        </NavLink>
                    </Menu.Item>
                    <SubMenu key="sub1" icon={<SettingOutlined />} title="Settings ">
                        <Menu.Item key="4">
                            <NavLink to="/bodytypes">
                                    Body Types
                            </NavLink>
                        </Menu.Item>
                        <Menu.Item key="5">
                            <NavLink to="/brands">
                                Brands
                            </NavLink>
                        </Menu.Item>
                        <Menu.Item key="6">
                            <NavLink to="/paymentypes">
                                Payment Types
                            </NavLink>
                        </Menu.Item>
                        <Menu.Item key="7">
                            <NavLink to="/documentypes">
                                Document Types
                            </NavLink>
                        </Menu.Item>
                    </SubMenu>
                    <SubMenu key="sub2" icon={<TeamOutlined />} title="User">
                        <Menu.Item key="8" onClick={auth.signout}>Log Out</Menu.Item>
                    </SubMenu>
                </Menu>
            </Sider>
            <Layout className="site-layout background-layout-color">
                <Header
                    className="site-layout-background"
                    style={{ padding: 0 }}
                />
                <Content style={{ margin: "0 16px" }}>
                    {children}
                </Content>
                <Footer className="background-layout-color" style={{ textAlign: "center" }}>
                    Created by Pablo Barrera
                </Footer>
            </Layout>
        </Layout>
    );
}

export default MainLayout;
