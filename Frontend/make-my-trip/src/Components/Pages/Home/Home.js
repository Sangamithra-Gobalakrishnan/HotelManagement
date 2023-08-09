import React,{useEffect} from "react";
import './Home.css';
import video from './hotel.mp4';
import { GrLocation } from 'react-icons/gr';
import { BsSearch } from 'react-icons/bs';
import { AiOutlineInstagram} from "react-icons/ai";
import { BsListTask } from "react-icons/bs";
import { SiTripadvisor } from "react-icons/si";
import {TbApps} from "react-icons/tb";
import {FiFacebook} from "react-icons/fi";
import {BsWallet2} from "react-icons/bs";
import Footer from "../../Common/Footer/Footer"; // Adjusted import path


import Aos from 'aos';
import 'aos/dist/aos.css';
import Portfolio from "../Portfolio/Portfolio";

const Home = () => {

    //Scroll animation

    useEffect(()=>{
        Aos.init({duration:2000})
    },[])

    return (
        <div>
        <section className='home'>
           <div className="overlay"></div>
           <video className="video" src={video} muted autoPlay loop type="video/mp4"></video>

            <div className="homeContent container-hotel">
                <div className="textDiv">

                    <span data-aos="fade-up" className="smallText">
                        Our Packages
                    </span>

                    <h1 data-aos="fade-up" className="homeTitle">
                        Enjoy your Holidays
                    </h1>
                </div>

                   <div data-aos="fade-up" className="cardDiv grid">
                       <div className="destinationInput">
                        <label className="lb" htmlFor="city">Enter your Country</label>
                        <div className="input flex">
                            <input className="input-box" type="text" placeholder="Enter country here"/>
                            <GrLocation className="icon"/>
                        </div>
                       </div>

                       <div className="dateInput">
                        <label className="lb" htmlFor="date">Enter your City</label>
                        <div className="input flex">
                            <input className="input-box" type="text" placeholder="Enter city here"/>
                            <GrLocation className="icon"/>
                        </div>
                       </div>

                       <div className="priceInput">
                         <div className="label_total flex">
                            <label className="lb" htmlFor="price">Max price</label>
                         </div>
                         <div className="input flex">
                            <input className="input-box" type="text" max="5000" min="1000"/>
                            <BsWallet2 className="icon"/>
                         </div>
                       </div>

                       <div className="searchOptions flex">
                            <BsSearch className="icon"/>
                            <span>SEARCH</span>
                       </div>
                    </div>

                    <div data-aos="fade-up" className="homeFooterIcons flex">
                        <div className="rightIcons">
                           <FiFacebook className="icon"/>
                           <AiOutlineInstagram className="icon"/>
                           <SiTripadvisor className="icon"/>
                        </div>

                        <div className="leftIcons">
                        <BsListTask className="icon"/>
                        <TbApps className="icon"/>
                       </div>
                    </div>
               </div>   
        </section>
        <Portfolio />
        <Footer />
        </div>
    )
}

export default Home;
