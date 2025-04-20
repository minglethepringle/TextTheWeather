import { useEffect, useState } from "react";
import { auth } from "../../firebase";
import { useNavigate } from "react-router-dom";
import { Fragment } from 'react'
import { Disclosure, Menu, Transition } from '@headlessui/react'
import { Bars3Icon, XMarkIcon } from '@heroicons/react/24/outline'
import AccountForm from "./AccountForm/AccountForm";

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
    }, [navigate]);

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
                    {({ }) => (
                        <>
                            <div className="mx-auto max-w-7xl px-4 sm:px-6 lg:px-8">
                                <div className="flex h-16 items-center justify-between">
                                    <div className="flex items-center">
                                        <div className="flex-shrink-0">
                                            
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