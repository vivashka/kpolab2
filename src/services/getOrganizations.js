export async function getOrganizations() {
    try {
        const url = process.env.REACT_APP_BASE_URL + process.env.REACT_GET_ORGANIZATIONS
        console.log(url)
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