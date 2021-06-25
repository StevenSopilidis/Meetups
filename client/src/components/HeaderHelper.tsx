/** @jsxImportSource @emotion/react **/
import { css } from "@emotion/react";
import { black1, gray2, red1, gray3, gray4, white2 } from "../Styles";
import { CgProfile } from "react-icons/cg";
import { GrGroup } from "react-icons/gr";
import { MdSettings } from "react-icons/md";
import { IconType } from "react-icons/lib";
import { IoExitOutline } from "react-icons/io5"; 

interface IHeaderHelperContainerProps 
{
    Icon: IconType;
    description: string;
    color: string;
}

//when the user clicks the arrow in the nav
const HeaderHelperContainer = ( { Icon, description, color}: IHeaderHelperContainerProps ) => (
    <div css={css`
        border-radius: 10%;
        width: 95%;
        height: 50px;
        display: flex;
        align-items: center;
        :hover{
            background-color: ${gray2}
        }
    `}>
        <Icon css={css`
            width: 20px;
            height: 20px;
            margin-left: 20px;
            color: ${color}
        `}/>
        <h5 css={css`
            margin-left: 50px;
            font-size: 14px;
            font-weight: 500;
            color: ${color}
        `}
        >
        {description}
        </h5>
    </div>
)

const HeaderHelper = () => (
    <div css={css`
        position: fixed;
        top: 80px;
        right: 50px;
        height: 250px;
        width: 200px;
        border-radius: 7%;
        background-color: ${white2};
        display: flex;
        flex-direction: column;
        justify-content: space-evenly;
        align-items: center;
        cursor: pointer;
    `}>
        <HeaderHelperContainer Icon={CgProfile} description={"My Profile"} color={gray4} />
        <HeaderHelperContainer Icon={GrGroup} description={"Group Chat"} color={gray4} />
        <HeaderHelperContainer Icon={MdSettings} description={"Settings"} color={black1} />
        
        <div css={css`
            background-color: ${gray3};
            height: 1px;
            width: 95%; 
        `}></div>
        
        <HeaderHelperContainer Icon={IoExitOutline} description={"Logout"} color={red1} />
    </div>
);

export default HeaderHelper;