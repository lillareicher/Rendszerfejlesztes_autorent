import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import NavMenu from "./NavMenu"

function CarDetails() {
    const [loading, setLoading] = useState(true);
    const [carsList, setCarsList] = useState([]);
    const [price, setPrice] = useState(0);
    const [rentsList, setRentsList] = useState([]);
    const [salesList, setSalesList] = useState([]);
    const [fromDate, setFromDate] = useState(" ");
    const [toDate, setToDate] = useState(" ");
    const [isAuth, setIsAuth] = useState(false);
    const params = useParams();
    const { carId } = params;
    const { username } = params;


    useEffect(() => {

        const token = localStorage.getItem('token');
        if (!token) {
            setIsAuth(false);
            setLoading(false);
            return;
        }

        setIsAuth(true);

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
                        <tr>
                            <td>{currentSale.description}</td>
                            <td>{currentSale.percentage}%</td>
                        </tr>
                    </tbody>
                </table>
            </div>            
        );
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
        if (data) {
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

    }

    if (loading) {
        return (<div>Loading data...</div>);
    }

    if (isAuth) {
        return (
            <div>
                <NavMenu username={username} />
                <div>
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