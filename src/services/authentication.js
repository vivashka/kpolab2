export async function authentication(user) {
    try{
        const url = process.env.REACT_APP_BASE_URL + process.env.REACT_USER_AUTHENTICATION

        const response = await fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(user)
        })

        return await response.json()
    }catch(err){
        console.log(e);
        return {
            success: false,
            message: "Не удалось подключиться к серверу",
            code: "400"
        };
    }
}