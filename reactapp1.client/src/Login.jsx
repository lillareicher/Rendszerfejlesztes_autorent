
import { useEffect, useState } from 'react';

function Login() {
    const [usernameC, setUsername] = useState();
    const [passwordC, setPassword] = useState();

    async function sendLoginInfo() {
        const data = {
            username: usernameC,
            password: passwordC
        };

        fetch('https://localhost:7045/api/auth/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data),
        })
            .then((response) => {
                console.log(response);
                if (!response.ok) {
                    window.alert("Invalid login information.");
                    throw new Error('Invalid username or password in frontend');
                }

                const responseData = response.json();
                const token = responseData.token;
                console.log(token);

                localStorage.setItem('token', token); // token elmentese localStorage-be

                //window.location.href = `/${usernameC}/cars`;
            })

            .catch(error => {
                console.log(error);
            });
    }

    function userChange(event) {
        setUsername(event.target.value);
    }
    function passChange(event) {
        setPassword(event.target.value);
    }

    return (
        <div>
            <h1 id="tabelLabel">Login</h1>
            <div>
                <div>
                    <label>Username:</label>
                    <input type="text" name="username" onChange={userChange} />
                </div>
                <div>
                    <label>Password:</label>
                    <input type="password" name="password" onChange={passChange} />
                </div>
                <button onClick={sendLoginInfo}>Login</button>
            </div>
        </div>
    );
}

export default Login;