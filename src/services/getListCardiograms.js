export async function getListCardiograms(filter) {
    try {
        const url = process.env.REACT_APP_BASE_URL + process.env.REACT_GET_LIST_CARDIOGRAMS
        console.log(url);
        const response = await fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(filter)
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