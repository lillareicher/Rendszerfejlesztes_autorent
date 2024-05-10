import { useEffect, useState, useRef } from 'react';
import { useParams } from 'react-router-dom';
import NavMenu from "./NavMenu"
import { jwtDecode } from "jwt-decode";

function CarDetails() {
    const [loading, setLoading] = useState(true);
    const [carsList, setCarsList] = useState([]);
    const [price, setPrice] = useState(0);
    const [rentsList, setRentsList] = useState([]);
    const [salesList, setSalesList] = useState([]);
    const [fromDate, setFromDate] = useState(" ");
    const [toDate, setToDate] = useState(" ");
    const [isAuth, setIsAuth] = useState(false);
    const [decToken, setDecToken] = useState(null);
    const [desc, setDesc] = useState(" ");
    const [perc, setPerc] = useState(" ");
    const params = useParams();
    const [message, setMessage] = useState(<div></div>);
    const ws = useRef(null);
    const { carId } = params;
    const { username } = params;


    useEffect(() => {

        const token = localStorage.getItem(`token_${username}`);
        if (!token) {
            setIsAuth(false);
            setLoading(false);
            return;
        }

        setIsAuth(true);

        const decoded = jwtDecode(token);
        decoded.role = decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
        decoded.username = decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
        delete decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
        delete decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
        setDecToken(decoded);

        async function fetchData() {
            try {
                const [carsResponse, rentsResponse, salesResponse] = await Promise.all([
                    fetch('https://localhost:7045/api/car/listcars').then(response => response.json()),
                    fetch('https://localhost:7045/api/rental/getrentals/' + carId).then(response => response.json()),
                    fetch('https://localhost:7045/api/sales/listsales').then(response => response.json())
                ]);

                setCarsList(carsResponse);
                setRentsList(rentsResponse);
                setSalesList(salesResponse);
                setLoading(false);
            } catch (error) {
                console.error('Error fetching data:', error);
                setLoading(false);
            }
        }

        if (!ws.current) {
            ws.current = new WebSocket('wss://localhost:7045/ws');
            ws.current.onmessage = event => {
                const mess = event.data;
                setMessage(<table
                    style={{ backgroundColor: 'rgb(255, 255, 153)', color: 'red' }}
                    border="1">
                    <tbody>
                        <tr>
                            <td style={{ verticalAlign: 'top' }}><b>{mess}</b><br /></td>
                            <td
                                style={{ verticalAlign: 'top', backgroundColor: 'rgb(255, 255, 153)' }}>
                                Please refresh the page to view all changes!<br />
                            </td>
                        </tr>
                    </tbody>
                </table>);
            };
        }

        fetchData();
    }, [carId]);

    function printCar() {
        const currentCar = carsList.find(car => car.id == carId);

        return (
            <tr key={currentCar.id}>
                <td>{currentCar.id}</td>
                <td>{currentCar.brand}</td>
                <td>{currentCar.model}</td>
                <td>{currentCar.categoryId}</td>
                <td>{currentCar.dailyPrice + "$"}</td>
            </tr>
        );
    }

    function listSales() {
        const currentSale = salesList.find(sale => sale.carId == carId);
        if (currentSale == null) {
            return (
                <div></div>
            );
        }
        return (
            <div>
                Current discounts:
                <table>
                    <thead>
                        <td>Description</td>
                        <td>Percentage</td>
                    </thead>
                    <tbody>
                        {listingSales()}
                    </tbody>
                </table>
            </div>            
        );
    }

    function listingSales() {
        var result = new Array();

        result = result.concat(salesList.map(sale => {
            return (
                <tr key={sale.id}>
                    <td>{sale.description}</td>
                    <td>{sale.percentage}%</td>
                </tr>
            );
        }));

        return result;
    }

    function listRents() {
        var result = new Array();

        result = result.concat(rentsList.map(rent => {
            return (
                <tr key={rent.id}>
                    <td>{(rent.fromDate).substring(0, 10)}</td>
                    <td>{(rent.toDate).substring(0, 10)}</td>
                </tr>
            );
        }));

        return result;
    }

    async function fromChange(event) {
        await setFromDate(event.target.value);
        await setFromDate(event.target.value);

    }
    async function toChange(event) {
        await setToDate(event.target.value);
        await setToDate(event.target.value);

    }

    async function makeReserv() {

        console.log(carId);

        const response = await fetch('https://localhost:7045/api/rental/newreservation?userName=' + username + '&carId=' + carId + '&_fromDate=' + fromDate + '&_toDate=' + toDate);
        const data = await response.json();
        console.log(data);

        window.location.reload();
    }



    async function sendReserv() {


        const response = await fetch('https://localhost:7045/api/rental/validdate?carId=' + carId + '&_fromDate=' + fromDate + '&_toDate=' + toDate);
        const data = await response.json();
        if (data && fromDate != " " && toDate != " ") {
            makeReserv();
            window.alert("Your reservation has been succesful!");
        } else {
            window.alert("Invalid reservation. Please check the dates.");
        }

    }

    async function countPrice() {

        const response = await fetch('https://localhost:7045/api/rental/countprice?carId=' + carId + '&_fromDate=' + fromDate + '&_toDate=' + toDate);
        const data = await response.json();

        setPrice(data);
        if (data == 0) {
            window.alert("Problem with the dates you picked.");
            return;
        }
    }

    function sendNewSale() {
        if (descChange != " " || percChange != " ") {
            const token = localStorage.getItem(`token_${username}`);
            const data = {
                Description: desc,
                Percentage: perc,
                CarId: carId
            };
            fetch('https://localhost:7045/api/sales/addsale', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`
                },
                body: JSON.stringify(data),
            }).then((response) => {
                if (response.ok) {
                    window.alert("New discount added succesfully!");
                    window.location.reload();
                } else if (response.status === 403) {
                    window.alert("Problem with adding new discount.");
                }
            });
        } else {
            window.alert("Problem with input data.");
        }
    }

    function descChange(event) {
        setDesc(event.target.value);
    }

    function percChange(event) {
        setPerc(event.target.value);
    }

    function printNewDiscount() {
        if (decToken.role == "Admin") {
            return (
                <fieldset>
                    Add new discount:<br></br>
                    Description:<br></br>
                    <input onChange={descChange}></input><br></br>
                    Percentage:<br></br>
                    <input onChange={percChange}></input><br></br>
                    <button onClick={sendNewSale }>Add</button><br></br>
                </fieldset>
            );
        }
        return (<div></div>)
    }

    if (loading) {
        return (<div>Loading data...</div>);
    }

    if (isAuth) {
        return (
            <div>
                <NavMenu username={username} />
                <div>
                    {message}
                    About this car:
                    <table border="1">
                        <thead>
                            <tr>
                                <td >Car Id</td>
                                <td >Brand</td>
                                <td >Model</td>
                                <td >Category Id</td>
                                <td >Daily Price</td>
                            </tr>
                        </thead>
                        <tbody>
                            {printCar()}
                        </tbody>
                    </table>
                </div>

                <br></br>
                <div>
                    Reserved on these dates:
                    <table border="1">
                        <thead>
                            <tr>
                                <td >From</td>
                                <td >To</td>
                            </tr>
                        </thead>
                        <tbody>
                            {listRents()}
                        </tbody>
                    </table>

                </div>

                {listSales()}

                <div>
                    Make a reservation here:
                    <br></br>
                    <label>Start date:</label> <input name="startDate" type="date" onChange={fromChange}></input>
                    <br></br>
                    <label>End date:</label> <input name="endDate" type="date" onChange={toChange}></input>
                    <br></br>
                    <button onClick={sendReserv}>Reserve</button>
                    <button onClick={countPrice}>Count price</button>
                    <h3>{price}$</h3>
                </div>

                {printNewDiscount()}

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

export default CarDetails;