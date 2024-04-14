import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import NavMenu from "./NavMenu"

function User() {
    const params = useParams();
    const { username } = params;
    const [user, setUser] = useState({id: "", uerName: "", name: "", password: ""});
    const [userRent, setUserRent] = useState([]);
    const flag = null;


    async function getUser() {
        const response = await fetch('https://localhost:7045/api/auth/getuser/' + username);
        const data = await response.json();
        setUser(data);
 
    }
    async function getUserRents() {
        const response = await fetch('https://localhost:7045/api/rental/getuserrentals/' + user.id);
        const data = await response.json();
        setUserRent(data);
    }

    useEffect(() => {
        getUser();
    }, [flag]);

    function listingRents() {
        getUserRents();
        var result = new Array();

        result = result.concat(userRent.map(rent => {
            return (
                <tr key ={rent.id} >
                    <td>{rent.id}</td>
                    <td></td>
                    <td>{(rent.fromDate).substring(0,10)}</td>
                    <td>{(rent.toDate).substring(0, 10)}</td>
                    <td>{(rent.created).substring(0, 10)}</td>
                    <td></td>
                    <td>{rent.carId}</td>
                </tr>
            );
        }));

        return result;
    }




    return (
        <div>
            <NavMenu username={ username} />

            <h2>Hello, {user.name}!</h2>
            <u>Account information:</u>
            <br></br>
            <b>Username:</b> {user.userName}
            <br></br>
            Rents:
            <table border = "1">
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
}

export default User;