export async function saveCardiogram(cardiogram) {
    try {
        const url = process.env.REACT_APP_BASE_URL + process.env.REACT_SAVE_CARDIOGRAM
        console.log(url);
        const response = await fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(cardiogram)
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