import { Route, Routes } from "react-router-dom"
import {FilePage, LoginPage, SignUpPage} from "../pages"
import { PublicRoute} from "./PublicRoute"
import { PrivateRoute } from "./PrivateRoute"
import { Pruebas } from "../pages/Pruebas"
import { NewPage } from "../pages/NewPage"
import { ProfilePage } from "../pages/ProfilePage"
import { SharedPage } from "../pages/SharedPage"
import { RecyclePage } from "../pages/RecyclePage"
import { FolderPage } from "../pages/FolderPage"

export const AppRouter = () => {
  return (
    <>
        <Routes>
            <Route path="/login" element={
                
                    <LoginPage />
                
            } />
            <Route path="/signup" element={
                
                    <SignUpPage />
                
                
            } />
            <Route path="/prueba" element={
                <Pruebas/>
            } />
            <Route path="/" element={
                <PrivateRoute>
                    <FilePage />
                </PrivateRoute>
            } />
            <Route path="/new" element={
                <NewPage/>
            } />
            <Route path="/recycle" element={
                <RecyclePage/>
            } />
            <Route path="/profile" element={
                <ProfilePage/>
            } />
            <Route path="/shared" element={
                <SharedPage/>
            } />

            <Route path="/folder/:id" element={
                <FolderPage />
            } />

        </Routes>
    </>
  )
}
