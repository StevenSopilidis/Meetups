/**@jsxImportSource @emotion/react **/
import React from "react";
import { css } from "@emotion/react";

interface IProps
{
    children: React.ReactNode;  
}

export const Page = ({ children }: IProps) => (
    <div css={css`
        margin-top: 70px;
    `}>
        {children}
    </div>
)