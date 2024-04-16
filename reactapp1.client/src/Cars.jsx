import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import NavMenu from "./NavMenu"
import { useParams } from 'react-router-dom';



function Cars() {
    const params = useParams();
    const { username } = params;
    const [loading, setLoading] = useState(true);
    const [carsList, setCarsList] = useState([]);
    const [categoryList, setCatList] = useState([]);
    const [filterCat, setFilterCat] = useState("Race");

    const flag = null;

    useEffect(() => {

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


        //setLoading(true);
        //const data = filterCat;
        //console.log("sendCategory(), data");
        //console.log(data);
        //console.log(JSON.stringify(data));

        const response = await fetch('https://localhost:7045/api/car/filtercars/' + filterCat);
        const data = await response.json();
        setCarsList(data);


        //const response = await fetch('https://localhost:7045/api/car/listcars');
        //const data = await response.json();
        //setCarsList(data);
        //setLoading(false);

        //fetch('https://localhost:7045/api/car/filtercars/' + filterCat ).then((response) => {
        //    console.log("sendCategory response");
        //    console.log(response);

        //    const res = await response.json();

        //    console.log("sendCategory res");
        //    console.log(res);

        //    setCarsList(res);
        //    setLoading(false);
        //}).catch(error => {
        //    console.log(error);
        //});
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

        </div>
    );

}

export default Cars;