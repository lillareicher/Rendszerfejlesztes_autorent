import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import NavMenu from "./NavMenu"
import { useParams } from 'react-router-dom';
import { jwtDecode } from "jwt-decode";



function Cars() {
    const params = useParams();
    const { username } = params;
    const [loading, setLoading] = useState(true);
    const [carsList, setCarsList] = useState([]);
    const [categoryList, setCatList] = useState([]);
    const [filterCat, setFilterCat] = useState("Race");
    const [isAuth, setIsAuth] = useState(false);
    const [decToken, setDecToken] = useState(null);
    const [brand, setBrand] = useState("");
    const [model, setModel] = useState("");
    const [dailyP, setDailyP] = useState("");

    const flag = null;

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
            setLoading(false);
            return;
        }
        setIsAuth(true);

        async function getCarsList() {

            const response = await fetch('https://localhost:7045/api/car/listcars');
            const data = await response.json();
            setCarsList(data);
            setLoading(false);
        }

        async function getCatList() {

            const response = await fetch('https://localhost:7045/api/category/listcategories');
            const data = await response.json();
            setCatList(data);
            setLoading(false);
        }

        getCarsList();
        getCatList();
    }, [flag]);

    //function accessDeniedMsg() {

    //}

    function brandChange(event) {
        setBrand(event.target.value);
    }
    function modelChange(event) {
        setModel(event.target.value);
    }
    function dailyPChange(event) {
        setDailyP(event.target.value);
    }

    async function sendNewCar() {
        const data = {
            
        };
    }


    function printAddNewCar() {
        if (decToken.role == "Admin") {
            return (
                <fieldset>
                    Add a new car:<br></br>
                    Brand:<br></br>
                    <input name="carBrand" onChange={brandChange}></input><br></br>
                    Model:<br></br>
                    <input name="carModel" onChange={modelChange}></input><br></br>
                    Daily Price:<br></br>
                    <input name="dailyPrice" onChange={dailyPChange}></input><br></br>
                    Category:<br></br>
                    <select name="categoryList" onChange={catChange}>
                        {selecting()}
                    </select><br></br>
                    <button>Add</button>

                </fieldset>
            );
        }
        return (<div></div>);
    }


    function listing() {
        var result = new Array();

        result = result.concat(carsList.map(car => {
            return (
                <tr key={car.id}>
                    <td>{car.id}</td>
                    <td>{car.brand}</td>
                    <td>{car.model}</td>
                    <td>{car.dailyPrice + "$"}</td>
                    <td><Link to={`/${username}/cars/` + car.id}>Open</Link></td>
                </tr>
            );
        }));

        return result;
    }

    async function sendCategory() {
        console.log("sendCategory(), filterCat");
        console.log(filterCat);



        const response = await fetch('https://localhost:7045/api/car/filtercars/' + filterCat);
        const data = await response.json();
        setCarsList(data);
    }

    function catChange(event) {
        if (event.target.value == "none") {
            window.location.reload();
        }
        console.log(event.target.value);
        setFilterCat(event.target.value);
        console.log("catChange(), filterCat");
        console.log(filterCat);
    }

    function selecting() {
        var result = new Array();

        result = result.concat(categoryList.map(cat => {
            return (
                <option value={cat.name} key={cat.id}>{cat.name}</option>
            );
        }));
        return result;
    }

    if (loading) {
        return <div>Loading data...</div>
    }

    if (isAuth) {
        return (
            <div>
                <NavMenu username={username} />
                <div>

                </div>
                <div>
                    <table border="1">
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Brand</th>
                                <th>Model</th>
                                <th>Daily Price</th>
                                <th>Link</th>
                            </tr>
                        </thead>
                        <tbody>
                            {listing()}
                        </tbody>
                    </table>
                    <select onChange={catChange}>
                        {selecting()}
                        <option value="none" >None</option>
                    </select>
                    <br></br>
                    <button onClick={sendCategory} >Filter</button>
                </div>

                {printAddNewCar()}



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

export default Cars;