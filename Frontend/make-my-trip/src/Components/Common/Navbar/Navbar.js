import React,{useState} from "react";
import {MdOutlineTravelExplore} from "react-icons/md";
import {AiFillCloseCircle} from "react-icons/ai";
import {TbGridDots} from "react-icons/tb";
import './Navbar.css';

const Navbar = () => {

   const [active, setActive] = useState('navBar');

   //Function to toggle Navbar
   const showNav  = () =>{
    setActive('navBar activeNavbar')
   }

   //Function to remove Navbar
   const removeNavbar = () =>{
     setActive('navBar')
   }
    return (
        <section className="navBarSection">
            <header className="header flex">

                <div className="logoDiv">
                    <a href="#" className="logo flex">
                        <h1><MdOutlineTravelExplore className="icon"/> MakeMyTrip.</h1>
                    </a>
                </div>

                <div className={active}>
                    <ul className="navLists flex">
                       <li className="navItem">
                         <a href="#" className="navLink">Home</a>
                       </li>
                       <li className="navItem">
                         <a href="#" className="navLink">About</a>
                       </li>
                       <li className="navItem">
                         <a href="#" className="navLink">Gallery</a>
                       </li>
                       <li className="navItem">
                         <a href="#" className="navLink">Packages</a>
                       </li>
                       <li className="navItem">
                         <a href="#" className="navLink">Hotels</a>
                       </li>
                       <li className="navItem">
                         <a href="#" className="navLink">Contact</a>
                       </li>
                       <button className="btn">
                         <a href="#">LOGIN</a>
                       </button>
                    </ul>

                    <div onClick={removeNavbar} className="closeNavbar">
                       <AiFillCloseCircle className="icon"/>
                    </div>
                </div>

                <div onClick={showNav} className="toggleNavbar">
                    <TbGridDots className="icon"/>
                </div>
                
            </header>
        </section>
    )
}

export default Navbar;
