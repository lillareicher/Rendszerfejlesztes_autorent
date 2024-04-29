import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import NavMenu from "./NavMenu"
import { jwtDecode } from "jwt-decode";

function User() {
    const params = useParams();
    const { username } = params;
    const [user, setUser] = useState({ id: "", uerName: "", name: "", password: "" });
    const [userRent, setUserRent] = useState([]);
    const [isAuth, setIsAuth] = useState(false);
    const [decToken, setDecToken] = useState(null);
    const flag = null;

    async function getUser() {
        const response = await fetch('https://localhost:7045/api/auth/getuser/' + username);
        const data = await response.json();
        setUser(data);

    }
    async function getUserRents() {
        const response = await fetch('https://localhost:7045/api/rental/getuserrentals?Username=' + username);
        const data = await response.json();
        setUserRent(data);
    }

    useEffect(() => {
        const token = localStorage.getItem('token');

        const decoded = jwtDecode(token);
        decoded.role = decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
        decoded.username = decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
        delete decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
        delete decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
        setDecToken(decoded);

        if (!token || decoded.username != username) {
            setIsAuth(false);
            return;
        }

        setIsAuth(true);
        getUser();
        getUserRents();
    }, [flag]);

    function listingRents() {
        var result = new Array();

        result = result.concat(userRent.map(rent => {
            return (
                <tr key={rent.id} >
                    <td>{rent.id}</td>
                    <td></td>
                    <td>{(rent.fromDate).substring(0, 10)}</td>
                    <td>{(rent.toDate).substring(0, 10)}</td>
                    <td>{(rent.created).substring(0, 10)}</td>
                    <td></td>
                    <td>{rent.carId}</td>
                </tr>
            );
        }));

        return result;
    }

    if (isAuth) {
        return (
            <div>
                <NavMenu username={username} />

                <h2>Hello, {username}!</h2>
                <u>Account information:</u>
                <br></br>
                <b>Username:</b> {username}
                <br></br>
                Rentals:
                <table border="1">
                    <thead>
                        <tr>
                            <td>Rental Id</td>
                            <td></td>
                            <td>From</td>
                            <td>To</td>
                            <td>Created</td>
                            <td></td>
                            <td>CarId</td>
                        </tr>
                    </thead>
                    <tbody>
                        {listingRents()}
                    </tbody>
                </table>
            </div>

        );
    } else {
        return (
            <div>
                <h1>Access Denied</h1>
                You do not have access to this page.
            </div>
        );
    }
}

export default User; 