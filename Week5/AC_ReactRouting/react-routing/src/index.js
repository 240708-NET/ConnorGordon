import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';

import App from './App';
import About from './pages/About';
import Contacts from './pages/Contacts';

import reportWebVitals from './reportWebVitals';
import { createBrowserRouter, RouterProvider, Link, useRouteError } from 'react-router-dom'

const router = createBrowserRouter([
  {errorElement: <ErrorBoundary />, children: [
    {path: '/', element: <App />},
    {path: '/about', element: <About />},
    {path: '/contacts', element: <Contacts />},
  ]}
])

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <RouterProvider router={router}></RouterProvider>
  </React.StrictMode>
);

function ErrorBoundary() {
  let error = useRouteError();
  console.error(error);

  return (
    <header>
      <Link to={"/"}>Home</Link>
      <h1>ERROR 404: Page not found</h1>
    </header>
  );
}

reportWebVitals();
