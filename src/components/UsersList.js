import { useState } from "react";
import { Button } from "devextreme-react/button";
import { UserView } from "./UserView";

export function UsersList({ users }) {
    const [editingUserId, setEditingUserId] = useState(null);

    return (
        <div className="users-list-container" style={{ padding: "20px", maxWidth: "800px" }}>
            <h1>Список пользователей</h1>
            {users.length === 0 ? (
                <div>Нет данных о пользователях</div>
            ) : (
                users.map((user) => (
                    <div
                        key={user.userUuid}
                        className="user-item"
                        style={{
                            marginBottom: "20px",
                            border: "1px solid #ccc",
                            padding: "15px",
                        }}
                    >
                        <UserView
                            user={user}
                            isModify={editingUserId === user.userUuid}
                        />
                        <Button
                            text={editingUserId === user.userUuid ? "Сохранить" : "Редактировать"}
                            onClick={() =>
                                setEditingUserId(
                                    editingUserId === user.userUuid ? null : user.userUuid
                                )
                            }
                            style={{ marginTop: "10px" }}
                        />
                    </div>
                ))
            )}
        </div>
    );
}
