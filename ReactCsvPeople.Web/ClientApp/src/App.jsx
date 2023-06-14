
import 'bootstrap/dist/css/bootstrap.min.css';
import React from 'react';
import { Route, Routes } from 'react-router-dom';
import Layout from './Layout';
import Home from './Home';
import Generate from './Generate';
import Upload from './Upload'


const App = () => {
    return (
        //<CandidateCountsContextComponent>
            <Layout>
                <Routes>
                    <Route exact path='/' element={<Home />} />
                    <Route exact path='/upload' element={<Upload />} />
                    <Route exact path='/generate' element={<Generate />} />

                </Routes>
            </Layout>
        //</CandidateCountsContextComponent>
    );
}

export default App;
