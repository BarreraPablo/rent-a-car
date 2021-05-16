import { Button, Card, Space, Table, Tooltip, message } from 'antd';
import React, { useEffect, useState } from 'react';
import { EditOutlined, EyeOutlined } from "@ant-design/icons";
import { NavLink, useHistory } from 'react-router-dom';
import { EDIT, SHOW } from './constants';
import * as reservationService from '../../services/reservationService';

function Reservation() {
    let history = useHistory();
    const [tableData, setTableData] = useState([])

    useEffect(() => {
        reservationService.getAll().then(res => {
            setTableData(res);
        }).catch(err => message.error(err));
    }, [])

    const columns = [
        {
            title: "ID",
            dataIndex: "id",
            key: "id",
        },
        {
            title: "Pick up",
            dataIndex: "pickUp",
            key: "pickUp",
            render: pickUp => pickUp.split('T')[0]
        },
        {
            title: "Drop off",
            dataIndex: "dropOff",
            key: "dropOff",
            render: dropOff => dropOff.split('T')[0]
        },
        {
            title: "Client",
            key: "client",
            dataIndex: "client",
            render: (client) => client.lastName + ", " + client.firstName,
        },
        {
            title: "Status",
            key: "status",
            dataIndex: "status"
        },
        {
            title: "User",
            dataIndex: "user",
            key: "user",
            render: (user) => user.username
        },
        {
            title: "Car",
            dataIndex: "car",
            key: "car",
            render: (car) => car.model
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
        history.replace(`/reservations/${action}/${id}`);
    };

    return (
        <div className="site-card-border-less-wrapper">
            <Card title="Reservation managment" bordered={false}>
                <NavLink to="/reservations/new">
                    <Button type="primary">Create new</Button> <br />
                    <br />
                </NavLink>
                <Table columns={columns} rowKey="id" dataSource={tableData} />
            </Card>
        </div>
    );
}

export default Reservation;