import React from 'react'
import ReactDOM from 'react-dom/client'
//import App from './App.jsx'
import Login from './Login.jsx'
import {
    createBrowserRouter,
    RouterProvider,
    //Route
} from "react-router-dom";
import './index.css'

const router = createBrowserRouter([
    {
        path: "/login",
        element: <Login />
    }
]);

ReactDOM.createRoot(document.getElementById('root')).render(
    <React.StrictMode>
        <RouterProvider router={router} />
    </React.StrictMode>,
)



