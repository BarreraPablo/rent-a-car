import { EditOutlined, EyeOutlined } from "@ant-design/icons";
import { Button, Card, Space, Table, Tooltip } from "antd";
import React, { useEffect, useState } from "react";
import { NavLink, useHistory } from "react-router-dom";
import * as clientService from "../../services/clientService";
import { SHOW, EDIT } from "./constants"

function Client() {
    const [tableData, setTableData] = useState([]);
    let history = useHistory();

    const columns = [
        {
            title: "Last Name",
            dataIndex: "lastName",
            key: "lastName",
        },
        {
            title: "First Name",
            dataIndex: "firstName",
            key: "firstName"
        },
        {
            title: "Document Number",
            dataIndex: "documentNumber",
            key: "documentNumber",
        },
        {
            title: "Phone Number",
            key: "phoneNumber",
            dataIndex: "phoneNumber",
        },
        {
            title: "Country",
            dataIndex: "country",
            key: "country",
            render: country => country.name
        },
        {
            title: "Birthday",
            dataIndex: "birthday",
            key: "birthday",
            render: birthday => birthday.split('T')[0]
        },
        {
            title: "Action",
            key: "action",
            render: (text, record) => (
                <Space size="middle">
                    <Tooltip title="Show">
                        <Button
                            shape="circle"
                            onClick={() => redirectTo(SHOW, record.id)}
                            icon={<EyeOutlined />}
                        />
                    </Tooltip>
                    <Tooltip title="Edit">
                        <Button
                            shape="circle"
                            onClick={() => redirectTo(EDIT, record.id)}
                            icon={<EditOutlined />}
                        />
                    </Tooltip>
                </Space>
            ),
        },
    ];

    const redirectTo = (action, id) => {
        history.replace(`/clients/${action}/${id}`);
    };

    useEffect(() => {
        clientService.getAll().then((info) => setTableData(info));
    }, []);

    return (
        <div className="site-card-border-less-wrapper">
            <Card title="Clients managment" bordered={false}>
                <NavLink to="/clients/new">
                    <Button type="primary">Create new</Button> <br />
                    <br />
                </NavLink>
                <Table columns={columns} rowKey="id" dataSource={tableData} />
            </Card>
        </div>
    );
}

export default Client;
