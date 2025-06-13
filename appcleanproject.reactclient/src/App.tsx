import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { Toaster } from "@/components/ui/sonner";
import AppLayout from '@/layouts/AppLayout';
import AuthLayout from '@/layouts/AuthLayout';
import HomePage from '@/pages/HomePage';
import RegisterPage from '@/pages/auth/RegisterPage';
import PrivateRoute from '@/components/PrivateRoute';
import ListPetsPage from '@/pages/pets/ListPetsPage';
import RegisterPetPage from '@/pages/pets/RegisterPetPage';
import LoginPage from '@/pages/auth/LoginPage';
import ServicesPage from '@/pages/catalogs/ServicesPage';
import SpecialitiesPage from '@/pages/catalogs/SpecialitiesPage';

function App() {

  return (
   <BrowserRouter>
    <Routes>
      <Route element={ <PrivateRoute><AppLayout /></PrivateRoute> }>
        <Route path='/' element={ <HomePage /> } />
        <Route path='/pets' element={ <ListPetsPage/> } />
        <Route path='/pets/register' element={ <RegisterPetPage /> } />
        <Route path='/catalog/services' element={ <ServicesPage /> } />
        <Route path='/catalog/specialities' element={ <SpecialitiesPage /> } />
      </Route>

      <Route element={ <AuthLayout /> }>
        <Route path='/auth/login' element={ <LoginPage /> } />
        <Route path='/auth/register' element={<RegisterPage />} />
      </Route>
    </Routes>
    <Toaster />
   </BrowserRouter>
  )
}

export default App;
