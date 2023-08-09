import React,{useEffect} from "react";
import './Gallery.css';


import img1 from './img1.jpg';
import img2 from './img2.jpg';
import img3 from './img3.jpg';
import img4 from './img4.jpg';
import img5 from './img5.jpg';
import img6 from './img6.jpg';
import img7 from './img7.jpg';
import img8 from './img8.jpg';



import Aos from 'aos';
import 'aos/dist/aos.css';

const Gallery = () => {

    useEffect(() => {
        Aos.init({ duration: 2000 });

        const allImages = document.querySelectorAll(".images .img");
        const lightbox = document.querySelector(".lightbox");
        const closeImgBtn = lightbox.querySelector(".close-icon");

        allImages.forEach(img => {
            img.addEventListener("click", () => showLightbox(img.querySelector("img").src));
        });

        const showLightbox = (img) => {
            lightbox.querySelector("img").src = img;
            lightbox.classList.add("show");
            document.body.style.overflow = "hidden";
        }

        closeImgBtn.addEventListener("click", () => {
            lightbox.classList.remove("show");
            document.body.style.overflow = "auto";
        });
    }, []);

    return (
        <section className="ga">
        <h1 className="img-h1" data-aos="fade-right">Image Gallery</h1>
        <div class="lightbox">
        <div class="wrapper">
            <header>
            <div class="details">
                <i class="uil uil-camera"></i>
                <span>Image Preview</span>
            </div>
            <div class="buttons"><i class="close-icon uil uil-times"></i></div>
            </header>
            <div class="preview-img">
            <div class="img"><img src={img1} alt="preview-img"/></div>
            </div>
        </div>
        </div>  
    <section class="gallery" data-aos="fade-up">
      <ul class="images">
        <li class="img"><img src={img1} alt="img"/></li>
        <li class="img"><img src={img2} alt="img"/></li>
        <li class="img"><img src={img3} alt="img"/></li>
        <li class="img"><img src={img5} alt="img"/></li>
        <li class="img"><img src={img4} alt="img"/></li>
        <li class="img"><img src={img6} alt="img"/></li>
        <li class="img"><img src={img7} alt="img"/></li>
        <li class="img"><img src={img8} alt="img"/></li>
      </ul>
       </section>
     </section>
    );
}

export default Gallery;
