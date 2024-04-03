import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import "./NavMenu.css";
//import 'bootstrap/dist/css/bootstrap.min.css'

function NavMenu() {
    return (
        <>
            <Navbar className="nav" bg="dark" data-bs-theme="dark">
                <Container>
                    <Navbar.Brand className = "title">Autorent</Navbar.Brand>
                    <Nav className="me-auto">
                        <ul>
                            <li>
                                <a>
                                    <Nav.Link href="/">Login</Nav.Link>
                                </a>
                            </li>
                            <li>
                                <a>
                                    <Nav.Link href="/cars">Cars</Nav.Link>
                                </a>
                            </li>
                            <li>
                                <a>
                                    <Nav.Link href="/user/John">User</Nav.Link>
                                </a>
                            </li>
                        </ul>
                    </Nav>
                </Container>
            </Navbar>
        </>
    );
}

export default NavMenu;