
import { useEffect, useState } from 'react';
//import Cookies from "universal-cookie";
//import jwt from "jwt-decode";

function Login() {
    const [usernameC, setUsername] = useState();
    const [passwordC, setPassword] = useState();
    //const cookies = new Cookies();

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
        }).then((response) => {
            if (!response.ok) {
                window.alert("Invalid login information.");
                throw new Error('Invalid username or password in frontend');
            }
            //console.log(response);

            return response.json();
        }).then((responseData) => {
            //console.log(responseData);
            localStorage.setItem('token', responseData);
            window.location.href = `/${usernameC}/cars`;
        }).catch(error => {
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