import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import NavMenu from "./NavMenu"
//import './cardetails.css';

function CarDetails() {
    const [loading, setLoading] = useState(true);
    const [carsList, setCarsList] = useState([]);
    const [price, setPrice] = useState(0);
    const [rentsList, setRentsList] = useState([]);
    const [fromDate, setFromDate] = useState(" ");
    const [toDate, setToDate] = useState(" "); 
    const [res, setRes] = useState(<div></div>);
    const params = useParams();
    const { carId } = params;


    useEffect(() => {

        async function getCarsList() {

            const response = await fetch('https://localhost:7045/api/car/listcars');
            const data = await response.json();
            setCarsList(data);
            setLoading(false);
        }

        async function getRents() {

            const response = await fetch('https://localhost:7045/api/rental/getrentals/' + carId);
            const data = await response.json();
            setRentsList(data);

        }

        getCarsList();
        getRents();
    }, []);

    function printCar() {
        const currentCar = carsList.find(car => car.id === carId);

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

    function listRents() {
        var result = new Array();

        result = result.concat(rentsList.map(rent => {
            return (
                <tr key={rent.id}>
                    <td>{(rent.fromDate).substring(0,10)}</td>
                    <td>{(rent.toDate).substring(0,10)}</td>
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
        const curDate = new Date();

        //onsole.log(curDate.getDate());
        var result = curDate.getFullYear() + "-";
        if (curDate.getMonth() < 9) {
            result += "0" + (curDate.getMonth()+1);
        } else {
            result += curDate.getMonth();
        }

        result += "-";

        if (curDate.getDay() < 10) {
            result += "0" + curDate.getDate();
        } else {
            result += curDate.getDate();
        }

        const response = await fetch('https://localhost:7045/api/rental/countprice?carId=' + carId + '&_fromDate=' + fromDate + '&_toDate=' + toDate);
        const data = await response.json();



        //var result = curDate.getFullYear() + "-" + curDate.getMonth() + "-" + curDate.getDay();

        setRes(<div>
            New reservation details:
            <table>
                <thead>
                    <tr>
                        <th>Username</th>
                        <th>CarId</th>
                        <th>From</th>
                        <th>To</th>
                        <th>Created</th>
                        <th>Price</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>John</td>
                        <td>{carId}</td>
                        <td>{fromDate}</td>
                        <td>{toDate}</td>
                        <td>{result}</td>
                        <td>{data}$</td>
                    </tr>
                </tbody>
            </table>
        </div>);

        //return (
            
        
        //);

    }


    //    const data = {
    //        userId: "John",
    //        carId: carId,
    //        fromDate: fromDate,
    //        toDate: toDate,
    //        created: "2024-03-31"
    //    };
    //    fetch('https://localhost:7045/api/rental/newreservation', {
    //        method: 'POST',
    //        headers: {
    //            'Content-Type': 'application/json',
    //        },
    //        body: JSON.stringify(data),
    //    }).then((response) => {
    //        if (!response.ok) {
    //            throw new Error('Reservation unsuccesful.');
    //        }

    //    }).catch(error => {
    //        console.log(error);
    //    });

    async function sendReserv() {


        const response = await fetch('https://localhost:7045/api/rental/validdate?carId=' + carId + '&_fromDate=' + fromDate + '&_toDate=' + toDate);
        //console.log(response);
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

    return (
        <div>
            <NavMenu/>
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
            {res}
        </div>
    );
}

export default CarDetails;