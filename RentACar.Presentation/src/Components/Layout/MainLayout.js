import { Layout, Menu, Breadcrumb } from 'antd';
import {
  DesktopOutlined,
  PieChartOutlined,
  FileOutlined,
  TeamOutlined,
  UserOutlined,
  CarOutlined
} from '@ant-design/icons';
import { useState } from 'react';
import { NavLink } from "react-router-dom";
import './MainLayout.css';

const { Header, Content, Footer, Sider } = Layout;
const { SubMenu } = Menu;

function MainLayout({children}) {
    const [collapsed, setCollapsed] = useState(false);

    const onCollapse = collapsed => {
        setCollapsed(collapsed);
    }
    console.log('re redenrs main layout')

    return (
        <Layout style={{ minHeight: "100vh", backgroundColor: "red" }}>
            <Sider
                collapsible
                collapsed={collapsed}
                onCollapse={onCollapse}
            >
                <div className="logo" />
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
                    <SubMenu key="sub1" icon={<UserOutlined />} title="User">
                        <Menu.Item key="3">Item1</Menu.Item>
                        <Menu.Item key="4">Item2</Menu.Item>
                        <Menu.Item key="5">Item3</Menu.Item>
                    </SubMenu>
                    <SubMenu key="sub2" icon={<UserOutlined />} title="Clients">
                        <Menu.Item key="6">Item1</Menu.Item>
                        <Menu.Item key="7">Item2</Menu.Item>
                        <Menu.Item key="8">Item3</Menu.Item>
                    </SubMenu>
                    <SubMenu key="sub3" icon={<TeamOutlined />} title="General">
                        <Menu.Item key="9">Brands</Menu.Item>
                        <Menu.Item key="10">Payment Methods</Menu.Item>
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
                    Creado por Pablo Barrera
                </Footer>
            </Layout>
        </Layout>
    );
}

export default MainLayout;