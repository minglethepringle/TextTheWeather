import { useEffect, useState } from "react";
import { auth } from "../../firebase";
import { useNavigate } from "react-router-dom";
import { Fragment } from 'react'
import { Disclosure, Menu, Transition } from '@headlessui/react'
import { Bars3Icon, XMarkIcon } from '@heroicons/react/24/outline'
import AccountForm from "./AccountForm/AccountForm";

// const navigation = [
//     // { name: 'Account', href: '#', current: true },
//     // { name: 'Team', href: '#', current: false },
//     // { name: 'Projects', href: '#', current: false },
//     // { name: 'Calendar', href: '#', current: false },
//     // { name: 'Reports', href: '#', current: false },
// ]

function classNames(...classes: any[]) {
    return classes.filter(Boolean).join(' ')
}

function AccountPage() {
    const navigate = useNavigate();
    const [userJwt, setUserJwt] = useState<string | null>(null);

    function signOut() {
        auth.signOut().then(() => {
            // Redirect to the sign-in page
            navigate("/");
        });
    }

    useEffect(() => {
        auth.onAuthStateChanged((user: any) => {
            if (user) {
                // Get the user's JWT token
                setUserJwt(user.accessToken);
            } else {
                // Redirect to the sign-in page
                navigate("/");
            }
        });

        // // Check if the user is signed in
        // if (!auth.currentUser) {
        //     // Redirect to the sign-in page
        //     navigate("/");
        // } else {
        //     // The user is signed in
        //     alert("User is signed in");
        //     console.log(auth.currentUser);
        //     // Get the user's JWT token
        //     auth.currentUser.getIdToken().then((token) => {
        //         setUserJwt(token);
        //     });
        // }
    }, [navigate]);

    useEffect(() => {
        // Check if the user's JWT token is set
        if (userJwt) {
            // Send the JWT token to the server
            // TODO: Implement this
            alert("User JWT token is set");
        }
    }, [userJwt]);

    return (
        <>
            {/*
                This example requires updating your template:
        
                ```
                <html class="h-full bg-gray-100">
                <body class="h-full">
                ```
              */}
            <div className="min-h-full">
                <Disclosure as="nav" className="bg-gray-800">
                    {({ open }) => (
                        <>
                            <div className="mx-auto max-w-7xl px-4 sm:px-6 lg:px-8">
                                <div className="flex h-16 items-center justify-between">
                                    <div className="flex items-center">
                                        <div className="flex-shrink-0">
                                            <img
                                                className="h-8 w-8"
                                                src="https://tailwindui.starxg.com/img/logos/mark.svg?color=indigo&shade=500"
                                                alt="Your Company"
                                            />
                                        </div>
                                        <div className="hidden md:block">
                                            <div className="ml-10 flex items-baseline space-x-4">
                                                {/* {navigation.map((item) => (
                                                    <a
                                                        key={item.name}
                                                        href={item.href}
                                                        className={classNames(
                                                            item.current
                                                                ? 'bg-gray-900 text-white'
                                                                : 'text-gray-300 hover:bg-gray-700 hover:text-white',
                                                            'rounded-md px-3 py-2 text-sm font-medium'
                                                        )}
                                                        aria-current={item.current ? 'page' : undefined}
                                                    >
                                                        {item.name}
                                                    </a>
                                                ))} */}
                                            </div>
                                        </div>
                                    </div>
                                    <div className="flex justify-end">
                                        <button
                                            onClick={signOut}
                                            className="bg-gray-900 text-white rounded-md px-3 py-2 text-sm font-medium"
                                        >
                                            Sign out
                                        </button>
                                    </div>
                                    {/* <div className="-mr-2 flex md:hidden">
                                        <Disclosure.Button className="relative inline-flex items-center justify-center rounded-md bg-gray-800 p-2 text-gray-400 hover:bg-gray-700 hover:text-white focus:outline-none focus:ring-2 focus:ring-white focus:ring-offset-2 focus:ring-offset-gray-800">
                                            <span className="absolute -inset-0.5" />
                                            <span className="sr-only">Open main menu</span>
                                            {open ? (
                                                <XMarkIcon className="block h-6 w-6" aria-hidden="true" />
                                            ) : (
                                                <Bars3Icon className="block h-6 w-6" aria-hidden="true" />
                                            )}
                                        </Disclosure.Button>
                                    </div> */}
                                </div>
                            </div>
                        </>
                    )}
                </Disclosure>

                <header className="bg-white shadow">
                    <div className="mx-auto max-w-7xl px-4 py-6 sm:px-6 lg:px-8">
                        <h1 className="text-3xl font-bold tracking-tight text-gray-900">My Account</h1>
                    </div>
                </header>
                <main>
                    <div className="mx-auto max-w-7xl py-10 px-4 lg:px-8">
                        <AccountForm />
                    </div>
                </main>
            </div>
        </>
    );
}

export default AccountPage;