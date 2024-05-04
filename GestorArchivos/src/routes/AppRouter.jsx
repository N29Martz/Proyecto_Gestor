import { Route, Routes } from "react-router-dom"
import { FilePage, LoginPage, SignUpPage } from "../pages"
import { PublicRoute } from "./PublicRoute"
import { PrivateRoute } from "./PrivateRoute"
//import { Pruebas } from "../pages/Pruebas"
//import { NewPage } from "../pages/NewPage"
import { ProfilePage } from "../pages/ProfilePage"
import { SharedPage } from "../pages/SharedPage"
import { RecyclePage } from "../pages/RecyclePage"
import { FolderPage } from "../pages/FolderPage"
import { RecordPage } from "../pages/RecordPage"

export const AppRouter = () => {
    return (
        <>
            <Routes>
                <Route path="/login" element={
                    <PublicRoute>
                        <LoginPage />
                    </PublicRoute>

                } />
                <Route path="/signup" element={
                    <PublicRoute>
                        <SignUpPage />
                    </PublicRoute>

                } />
                <Route path="/" element={
                    <PrivateRoute>
                        <FilePage />
                    </PrivateRoute>
                } />
                {/* <Route path="/new" element={
                    <PrivateRoute>
                        <NewPage />
                    </PrivateRoute>
                } /> */}
                <Route path="/recycle" element={
                    <PrivateRoute>
                        <RecyclePage />
                    </PrivateRoute>
                } />
                <Route path="/record" element={
                    <PrivateRoute>
                        <RecordPage />
                    </PrivateRoute>
                } />
                <Route path="/profile" element={
                    <PrivateRoute>
                        <ProfilePage />
                    </PrivateRoute>
                } />
                <Route path="/shared" element={
                    <PrivateRoute>
                        <SharedPage />
                    </PrivateRoute>
                } />

                <Route path="/folder/:id" element={
                    <PrivateRoute>
                        <FolderPage />
                    </PrivateRoute>
                } />
                {/* <Route path="/pruebas" element={
                    <PrivateRoute>
                        <Pruebas />
                    </PrivateRoute>
                } /> */}
            </Routes>
        </>
    )
}
