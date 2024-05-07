import { useEffect, useState } from 'react';
//import { jwtDecode } from "jwt-decode";
import { useParams } from 'react-router-dom';

function Logout() {
    //const [token, setToken] = useState();
    const params = useParams();
    const { username } = params;

    //localStorage.getItem(`token_${username}`);
    localStorage.removeItem(`token_${username}`);
    window.location.href = '/';

    //function removeToken() {
    //    if (token && token !== "") {
    //        const decoded = jwtDecode(token);
    //        var name = decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
    //        localStorage.removeItem(`token_${name}`);
    //    }

    //}

    //if (token !== undefined) {
    //    removeToken();
    //}
}

export default Logout;