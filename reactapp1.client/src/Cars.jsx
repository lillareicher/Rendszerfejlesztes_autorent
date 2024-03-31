import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';



function Cars() {
    const [carsList, setCarsList] = useState([]);
    //const [categoryList, setCatList] = useEffect([]);
    //const [filterCat, setFilterCat] = useState([]);

    const flag = null;

    useEffect(() => {

        async function getCarsList() {

            const response = await fetch('https://localhost:7045/api/car/listcars');
            const data = await response.json();
            setCarsList(data);
        }

        //async function getCatList() {

        //    const response = await fetch('https://localhost:7045/api/category/listcaregories');
        //    const data = await response.json();
        //    setCatList(data);
        //}


        getCarsList();
        //getCatList();
    }, [flag]);


    function listing() {
        var result = new Array();

        result = result.concat(carsList.map(car => {
            return (
                <tr key={car.id}>
                    <td>{car.id}</td>
                    <td>{car.brand}</td>
                    <td>{car.model}</td>
                    <td>{car.dailyPrice}</td>
                    <td><Link to={'/cars/' + car.id}>Open</Link></td>
                </tr>
            );
        }));
        return result;
    }

    //async function sendCategory() {
    //    const data = {
    //        filter: filterCat
    //    }

    //    //elküldjük a szükséges filter adatot
    //    fetch('https://localhost:7045/api/category/filtercaregories', {
    //        method: 'POST',
    //        headers: {
    //            'Content-Type': 'application/json',
    //        },
    //        body: JSON.stringify(data),
    //    }).then((response) => {
    //    //megkapjuk a frissített listát
    //        const res = response.json();
    //        setCarsList(res)
    //    //helyettesítjük a listát a frissített listával
    //    }).catch(error => {
    //        console.log(error);
    //    });

    //}

    function catChange(event) {
        if (event.target.value == "none") {
            window.location.reload();
        }
        //setFilterCat(event.target.value);
    }

    //function selecting() {
    //    var result = new Array();

    //    result = result.concat(categoryList.map(cat => {
    //        return (
    //            <option key={cat.id}>{cat.name}</option>
    //        );
    //    }));
    //    return result;
    //}

    return (
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
                <option value="five_seat">Five seat</option>
                <option value="off_road">Off-road</option>
                <option value="race_car">Race car</option>
                <option value="none">None</option>
                {/*{selecting()}*/}
            </select>
            <br></br>
            <button /*onClick={sendCategory}*/>Filter</button>
        </div>
    );

}

export default Cars;