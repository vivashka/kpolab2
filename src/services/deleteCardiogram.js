export async function deleteCardiogram(guid) {
    try {
        const url = process.env.REACT_APP_BASE_URL + process.env.REACT_DELETE_CARDIOGRAM
        console.log(url);
        const response = await fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(guid)
        })

        return await response.json()


    } catch (e) {
        console.log(e);
        return {
            success: false,
            message: "Не удалось подключиться к серверу",
            code: "400"
        };
    }
}