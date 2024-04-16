import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
//import { useParams } from 'react-router-dom';
import "./NavMenu.css";
import PropTypes from 'prop-types';
//import 'bootstrap/dist/css/bootstrap.min.css'

function NavMenu({ username }) {
    //const params = useParams();
    //const { userN } = params;

    return (
        <>
            <Navbar className="nav" bg="dark" data-bs-theme="dark">
                <Container>
                    <Navbar.Brand className="title">Autorent</Navbar.Brand>
                    <Nav className="me-auto">
                        <ul>
                            <li>
                                <a>
                                    <Nav.Link href="/">Login</Nav.Link>
                                </a>
                            </li>
                            <li>
                                <a>
                                    <Nav.Link href={`/${username}/cars`}>Cars</Nav.Link>
                                </a>
                            </li>
                            <li>
                                <a>
                                    <Nav.Link href={`/${username}`}>User</Nav.Link>
                                </a>
                            </li>
                        </ul>
                    </Nav>
                </Container>
            </Navbar>
        </>
    );
}

NavMenu.propTypes = {
    username: PropTypes.string.isRequired, // Validate username prop
};

export default NavMenu;