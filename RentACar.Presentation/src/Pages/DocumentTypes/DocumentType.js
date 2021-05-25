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
import * as documentTypeService from "../../services/documentTypeService";
import { EDIT, NEW } from "./constants";

function DocumentType() {
    const [form] = Form.useForm();
    const [data, setData] = useState([]);
    const [editingKey, setEditingKey] = useState("");
    const [loading, setLoading] = useState(false);

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
        documentTypeService
            .getAll()
            .then((res) => {
                setData(res);
            })
            .catch((err) => message.error(err));
    };

    const cancel = (id) => {
        if (id === -1) {
            const newData = [...data];
            const index = newData.findIndex((item) => id === item.id);
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
                documentTypeService
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
        const newData = {
            id: -1,
            name: "",
            description: "",
        };
        edit(newData);
        setData([newData, ...data]);
    };

    return (
        <div className="site-card-border-less-wrapper">
            <Card title="Document Types Managment" bordered={false}>
                <Button
                    onClick={handleAdd}
                    type="primary"
                    style={{
                        marginBottom: 16,
                    }}
                >
                    Add a Document Type
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
                            onChange: cancel,
                        }}
                    />
                </Form>
            </Card>
        </div>
    );
}

export default DocumentType;
