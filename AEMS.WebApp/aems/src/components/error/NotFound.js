import React, { useEffect, useRef } from "react";
import { Link } from "react-router-dom";
import lottie from "lottie-web";

function NotFound(props) {
  const container = useRef(null);

  useEffect(() => {
    lottie.loadAnimation({
      container: container.current,
      renderer: "svg",
      loop: true,
      autoplay: true,
      animationData: require("./notFound.json")
    });
  }, []);

  return (
          <div className="text-center " ref={container}>
          </div>
  );
}

export default NotFound;
