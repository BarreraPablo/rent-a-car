import {
    CloseCircleOutlined, EditOutlined,
    SaveOutlined
} from "@ant-design/icons";
import {
    Button,
    Card,
    Form,
    message,
    Popconfirm,
    Space,
    Table,
    Tooltip
} from "antd";
import React, { useEffect, useState } from "react";
import { EditableCell } from "../../Components/EditableCell/EditableCell";
import * as paymentTypeService from "../../services/paymentTypeService";
import { EDIT, NEW } from "./constants";

function PaymentType() {
    const [form] = Form.useForm();
    const [data, setData] = useState([]);
    const [editingKey, setEditingKey] = useState("");
    const [loading, setLoading] = useState(false);
    const [currentPage, setCurrentPage] = useState(1)

    useEffect(() => {
        refreshTable();
    }, []);

    const isEditing = (record) => record.id === editingKey;

    const edit = (record) => {
        form.setFieldsValue({
            ...record,
        });

        setEditingKey(record.id);
    };

    const refreshTable = () => {
        paymentTypeService
            .getAll()
            .then((res) => {
                setData(res);
            })
            .catch((err) => message.error(err));
    };

    const cancel = () => {
        const newData = [...data];
        const index = newData.findIndex((item) => -1 === item.id); // search for the not saved new record

        if(index !== -1) {
            newData.splice(index, 1);
            setData(newData);
        }

        setEditingKey("");
    };

    const save = async (key) => {
        try {
            setLoading(true);
            const row = await form.validateFields();
            const newData = [...data];

            const index = newData.findIndex((item) => key === item.id);

            if (index > -1) {
                const item = newData[index];

                const updatedRow = { ...item, ...row };

                newData.splice(index, 1, updatedRow);
                paymentTypeService
                    .save(updatedRow, item.id === -1 ? NEW : EDIT)
                    .then(() => {
                        refreshTable();
                        setEditingKey("");
                    })
                    .catch((err) => message.error(err))
                    .finally(() => setLoading(false));
            } else {
                message.error("Something gone wrong. Please try again later.");
            }
        } catch (errInfo) {
            console.log("Err:", errInfo);
        }
    };

    const columns = [
        {
            title: "Name",
            dataIndex: "name",
            editable: true,
        },
        {
            title: "Description",
            dataIndex: "description",
            editable: true,
        },
        {
            title: "Action",
            dataIndex: "action",
            width: "15%",
            render: (_, record) => {
                const editable = isEditing(record);
                return editable ? (
                    <Space size="small">
                        <Tooltip title="Save">
                            <Button
                                shape="circle"
                                loading={loading}
                                onClick={() => save(record.id)}
                                icon={<SaveOutlined />}
                            />
                        </Tooltip>
                        <Popconfirm
                            title="Sure to cancel?"
                            onConfirm={() => cancel(record.id)}
                        >
                            <Button
                                shape="circle"
                                icon={<CloseCircleOutlined />}
                            />
                        </Popconfirm>
                    </Space>
                ) : (
                    <Tooltip title="Edit">
                        <Button
                            disabled={editingKey !== ""}
                            shape="circle"
                            onClick={() => edit(record)}
                            icon={<EditOutlined />}
                        />
                    </Tooltip>
                );
            },
        },
    ];

    const mergedColumns = columns.map((col) => {
        if (!col.editable) {
            return col;
        }

        return {
            ...col,
            onCell: (record) => ({
                record,
                inputType: "text",
                dataIndex: col.dataIndex,
                title: col.title,
                editing: isEditing(record),
            }),
        };
    });

    const handleAdd = () => {
        setCurrentPage(1);
        const newData = {
            id: -1,
            name: "",
            description: "",
        };
        edit(newData);
        setData([newData, ...data]);
    };

    const onPageChange = (page) => {
        setCurrentPage(page);
        cancel();
    }

    return (
        <div className="site-card-border-less-wrapper">
            <Card title="Payment Types Managment" bordered={false}>
                <Button
                    onClick={handleAdd}
                    type="primary"
                    disabled={editingKey !== ""}
                    style={{
                        marginBottom: 16,
                    }}
                >
                    Add a Payment Type
                </Button>
                <Form form={form} component={false}>
                    <Table
                        components={{
                            body: {
                                cell: EditableCell,
                            },
                        }}
                        bordered
                        dataSource={data}
                        columns={mergedColumns}
                        rowClassName="editable-row"
                        pagination={{
                            onChange: onPageChange,
                            current: currentPage,
                        }}
                    />
                </Form>
            </Card>
        </div>
    );
}

export default PaymentType;
