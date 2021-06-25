/** @jsxImportSource @emotion/react **/
import { css } from "@emotion/react";
import React from "react";
import tweeter from '../tweeter.svg';
import { white2 ,NameH5, HeaderNavigationLink, HeaderNavigationContainer, HeaderNavigationContainerCurrent, HeaderNavigationLinkCurrent } from "../Styles";  
import { IoMdArrowDropdown, IoMdArrowDropup } from "react-icons/io";
import TestImage from "../TestImage.jpg";
import HeaderHelper from "./HeaderHelper";
import { Link, useLocation } from "react-router-dom";

const Header = ( ) => {
    const [arrowPressed, setArrowPressed] = React.useState(false);
    const [urlLocation, setUrlLocation] = React.useState("/");

    const location = useLocation();

    React.useEffect(() => {
        setUrlLocation(location.pathname);
    }, [location]);

    return (
        <div css={css`
            background-color: ${white2};
            height: 70px;
            width: 100vw;
            display: flex;
            align-items: center;
            justify-content: space-between;
            position: fixed;
            top: 0;
        `}
        >
            <div css={css`
                margin-left: 50px
            `}>
                <img src={tweeter}  alt="Website logo" />
            </div>

            <div css={css`
                height: 100%;
                display: flex;
                align-items: center;
            `}>

                {urlLocation === "/"?
                    <Link css={css`text-decoration: none;`} to="/">
                        <HeaderNavigationContainerCurrent>
                                <HeaderNavigationLinkCurrent>Home</HeaderNavigationLinkCurrent>
                        </HeaderNavigationContainerCurrent> 
                    </Link>
                    :
                    <Link css={css`text-decoration: none;`} to="/">
                        <HeaderNavigationContainer>
                            <HeaderNavigationLink>Home</HeaderNavigationLink>
                        </HeaderNavigationContainer>
                    </Link>
                }

                {urlLocation === "/explore"?
                    <Link css={css`text-decoration: none;`} to="/explore">
                        <HeaderNavigationContainerCurrent>
                            <HeaderNavigationLinkCurrent>Explore</HeaderNavigationLinkCurrent>
                        </HeaderNavigationContainerCurrent> 
                    </Link>
                    :
                    <Link css={css`text-decoration: none;`} to="/explore">
                        <HeaderNavigationContainer>
                                <HeaderNavigationLink>Explore</HeaderNavigationLink>
                        </HeaderNavigationContainer>
                    </Link>
                }

                {urlLocation === "/bookmarks"?
                    <Link css={css`text-decoration: none;`} to="/bookmarks">      
                        <HeaderNavigationContainerCurrent>
                                <HeaderNavigationLinkCurrent>Bookmarks</HeaderNavigationLinkCurrent>
                        </HeaderNavigationContainerCurrent> 
                    </Link>
                    :
                    <Link css={css`text-decoration: none;`} to="/bookmarks">
                        <HeaderNavigationContainer>
                            <HeaderNavigationLink>Bookmarks</HeaderNavigationLink>
                        </HeaderNavigationContainer>
                    </Link>
                }

            </div>

            <div css={css`
                height: 70px;
                width: 200px;
                display: flex;
                justify-content: space-around;
                align-items:center;
                margin-right: 40px;
            `}>
                <img
                alt="Your Profile Image"
                css={css`
                    height: 35px;
                    width: 40px;
                    border-radius: 25%;
                `}src={TestImage} />
                <NameH5>Steven Sopilidis</NameH5>
                {arrowPressed && 
                <IoMdArrowDropup 
                onClick={()=> setArrowPressed(false)}
                css={css`
                    cursor: pointer;
                `}/>
                }

                {!arrowPressed && 
                <IoMdArrowDropdown 
                css={css`
                    cursor: pointer;
                `}
                onClick={() => setArrowPressed(true)}
                />}
            </div>

            {arrowPressed && 
            <HeaderHelper />}
        </div>
    )
}

export default Header;