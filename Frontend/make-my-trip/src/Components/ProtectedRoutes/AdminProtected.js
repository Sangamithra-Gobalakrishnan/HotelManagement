import { Navigate } from "react-router-dom";

function AdminProtected({token,children})
{
    token=sessionStorage.getItem("token");
    if(token!=null)
        return children;
    return <Navigate to='/'/>
}

export default AdminProtected;