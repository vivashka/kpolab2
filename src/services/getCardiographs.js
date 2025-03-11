export async function getCardiographs(guid) {
    try {
        const url = process.env.REACT_APP_BASE_URL + process.env.REACT_GET_CARDIOGRAPHS + guid
        const response = await fetch(url, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
            }
        })


        if (response.ok) {
            return await response.json()
        } else {
            return await response.json()

        }

    } catch (e) {
        console.log(e);
        return {
            success: false,
            message: "Не удалось подключиться к серверу",
            code: "400"
        };
    }
}