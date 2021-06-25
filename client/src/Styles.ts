import styled from "@emotion/styled";

export const white1 = "#f5f5f5"

export const white2 = "#FFFFFF"

export const gray1 = "#f0ecec";

export const gray2 = "#F2F2F2";

export const gray3 = "#adadad";

export const gray4 = "#4F4F4F";

export const black1= "#333333";

export const red1 = "#ed2b2b";

export const blue1 = "#2F80ED";

export const NameH5 = styled.h5`
    font-family: sans-serif;
    color: #232323;
    font-weight: bold;
    font-size: 14px;
    color: ${black1};
`

export const HeaderNavigationContainer = styled.div
`
    cursor: pointer;
    :hover{
        border-bottom: 3px solid ${blue1};
        color: ${blue1}
    }
`
export const HeaderNavigationContainerCurrent = styled.div
`
    cursor: pointer;
    color: ${blue1};
    border-bottom: 3px solid ${blue1};
`

export const HeaderNavigationLink = styled.h5`
    margin-left: 13px;
    margin-right: 13px;
    font-size: 17px;
    color: #828282;
    font-family: sans-serif;
`

export const HeaderNavigationLinkCurrent = styled.h5`
    margin-left: 13px;
    margin-right: 13px;
    font-size: 17px;
    color: ${blue1};
    font-family: sans-serif;
`