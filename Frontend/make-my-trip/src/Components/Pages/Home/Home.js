import React from "react";
import './Home.css';
import video from './mountains.mp4';
import {GrLocation} from 'react-icons/gr';
import {HiFilter} from 'react-icons/hi';
import { AiOutlineInstagram } from "react-icons/ai";
import { TbApps } from "react-icons/tb";
import { BsListTask } from "react-icons/bs";
import { SiTripadvisor } from "react-icons/si";
import { FiFacebook } from "react-icons/fi";

const Home = () => {

    return (
        <section className='home'>
           <div className="overlay"></div>
           <video className="video" src={video} muted autoPlay loop type="video/mp4"></video>

            <div className="homeContent container">
                <div className="textDiv">

                    <span className="smallText">
                        Our Packages
                    </span>

                    <h1 className="homeTitle">
                        Search your Holidays
                    </h1>
                </div>

                   <div className="cardDiv grid">
                       <div className="destinationInput">
                        <label className="lb" htmlFor="city">Search your Destination</label>
                        <div className="input flex">
                            <input className="input-box" type="text" placeholder="Enter location here"/>
                            <GrLocation className="icon"/>
                        </div>
                       </div>

                       <div className="dateInput">
                        <label className="lb" htmlFor="date">Select your Date</label>
                        <div className="input flex">
                            <input className="input-box" type="date"/>
                        </div>
                       </div>

                       <div className="priceInput">
                         <div className="label_total flex">
                            <label className="lb" htmlFor="price">Max price:</label>
                            <h3 className="total">Rs.5000</h3>
                         </div>
                         <div className="input flex">
                            <input className="input-box" type="range" max="5000" min="1000"/>
                         </div>
                       </div>

                       <div className="searchOptions flex">
                            <HiFilter className="icon"/>
                            <span>FILTER YOUR NEED</span>
                       </div>
                    </div>

                    <div className="homeFooterIcons flex">
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
    )
}

export default Home;
