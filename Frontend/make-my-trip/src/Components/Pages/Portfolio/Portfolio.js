import React,{useEffect} from "react";
import "./Portfolio.css";

// Imported Assets
import icon1 from './icon1.png';
import icon2 from './icon2.png';
import icon3 from './icon3.png';
import sideimage from './side-image.jpeg';

import Aos from 'aos';
import 'aos/dist/aos.css';

const Portfolio = () => {

  //Scroll animation

  useEffect(()=>{
    Aos.init({duration:2000})
  },[])

  return (
    <div className="portfolio container-hotel  section " id="portfolio">
      <div data-aos="fade-up" className="secContainer grid-port">
        <div className="leftContent">
          <div className="secHeading">
            <h3 data-aos="fade-right" className="h3">Why Should You Choose Us</h3>
            <p className="p">
              Discover the reasons why our hotel stands out in providing a memorable and comfortable stay.
            </p>
          </div>

          <div data-aos="fade-up" className="grid-port">
            <div className="singlePortfolio flex">
              <div className="iconDiv">
                <img className="icon-icon" src={icon1} alt="Safety and Support Icon" />
              </div>
              <div className="infor">
                <h4 className="h4">Safety and Support</h4>
                <p className="p">
                  Your safety and well-being are our top priorities. We maintain high safety standards and have dedicated staff available for any assistance during your stay.
                </p>
              </div>
            </div>
          </div>

          <div data-aos="fade-up" className="grid-port">
            <div className="singlePortfolio flex">
              <div className="iconDiv">
                <img className="icon-icon" src={icon2} alt="Diverse Range Of Destinations Icon" />
              </div>
              <div className="infor">
                <h4 className="h4">Diverse Range Of Destinations</h4>
                <p className="p">
                  Explore a wide range of destinations with our hotel. Whether it's a relaxing getaway or an adventurous journey, we cater to different interests and preferences.
                </p>
              </div>
            </div>
          </div>

          <div data-aos="fade-up" className="grid-port">
            <div className="singlePortfolio flex">
              <div className="iconDiv">
                <img className="icon-icon" src={icon3} alt="24/7 Customer Support Icon" />
              </div>
              <div className="infor">
                <h4 className="h4">24/7 Customer Support</h4>
                <p className="p">
                  Our dedicated customer support team is available round the clock to address any queries or concerns, ensuring a seamless experience before, during, and after your stay.
                </p>
              </div>
            </div>
          </div>
        </div>
        
        <div className="rightContent">
          <img className="icon-icon" src={sideimage} alt="Hotel Side Image" />
        </div>
      </div>
    </div>
  );
};

export default Portfolio;
