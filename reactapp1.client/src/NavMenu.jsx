import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import "./NavMenu.css";
import PropTypes from 'prop-types';

function NavMenu({ username }) {

    return (
        <>
            <Navbar className="nav" bg="dark" data-bs-theme="dark">
                <Container>
                    <Navbar.Brand className="title">Autorent</Navbar.Brand>
                    <Nav className="me-auto">
                        <ul>
                            <li>
                                <a>
                                    <Nav.Link href="/">Logout</Nav.Link>
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
    username: PropTypes.string.isRequired,
};

export default NavMenu;