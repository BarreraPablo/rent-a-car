import { EditOutlined, EyeOutlined } from "@ant-design/icons";
import { Button, Card, Space, Table, Tooltip } from "antd";
import React, { useEffect, useState } from "react";
import { NavLink, useHistory } from "react-router-dom";
import * as carService from "../../services/carService";
import "./Cars.css";
import { SHOW, EDIT } from "./constants"

function Cars() {
    const [tableData, setTableData] = useState([]);
    let history = useHistory();

    const columns = [
        {
            title: "Model",
            dataIndex: "model",
            key: "model",
        },
        {
            title: "Brand",
            dataIndex: "brand",
            key: "brand",
            render: (brand) => brand.name,
        },
        {
            title: "Year",
            dataIndex: "year",
            key: "year",
        },
        {
            title: "Body Type",
            key: "bodyType",
            dataIndex: "bodyType",
            render: (bodyType) => bodyType.name,
        },
        {
            title: "Seats",
            dataIndex: "seats",
            key: "seats",
        },
        {
            title: "Price Per Day",
            dataIndex: "pricePerDay",
            key: "pricePerDay",
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
        history.replace(`/cars/${action}/${id}`);
    };

    useEffect(() => {
        carService.getCars().then((info) => setTableData(info));
    }, []);

    return (
        <div className="site-card-border-less-wrapper">
            <Card title="Cars managment" bordered={false}>
                <NavLink to="/cars/new">
                    <Button type="primary">Create new</Button> <br />
                    <br />
                </NavLink>
                <Table columns={columns} rowKey="id" dataSource={tableData} />
            </Card>
        </div>
    );
}

export default Cars;
