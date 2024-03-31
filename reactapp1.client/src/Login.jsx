
import { useEffect, useState } from 'react';
//import './App.css';

function Login() {
    const [usernameC, setUsername] = useState();
    const [passwordC, setPassword] = useState();

    async function sendLoginInfo() {
        const data = {
            username: usernameC,
            password: passwordC
        };

        fetch('https://localhost:7045/api/auth/login', { // külön fájlba kiszervezni const-ként a lekéréseket
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data),
        })
            .then((response) => {
                if (!response.ok) {
                    throw new Error('Invalid username or password in frontend');
                }

                window.location.href = "/cars";
                //return response.json();
            })
            //.then((responsedata) => {
            //    localStorage.setItem('token', responsedata.Token);
            //    
            //})
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
