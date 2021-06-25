import React from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Header from "./components/Header";
import { BookMarksPage } from './Pages/BookMarksPage';
import { ExplorePage } from './Pages/ExplorePage';
import { HomePage } from './Pages/HomePage';

function App() {
  return (
    <BrowserRouter>
      <div>
          <Header />
          <Routes>
            <Route path="/" element={<HomePage />}></Route>
            <Route path="/explore" element={<ExplorePage />}></Route>
            <Route path="/bookmarks" element={<BookMarksPage />}></Route>
          </Routes>
      </div>
    </BrowserRouter>
  );
}

export default App;
