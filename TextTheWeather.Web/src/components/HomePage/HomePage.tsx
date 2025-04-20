import { Fragment, useEffect, useState } from 'react'
import { Dialog, Transition } from '@headlessui/react'
import { Bars3Icon, ExclamationTriangleIcon, XMarkIcon } from '@heroicons/react/24/outline'
import { ChevronRightIcon } from '@heroicons/react/20/solid'
import { on } from 'events'
import { RecaptchaVerifier, getAuth, signInWithPhoneNumber } from 'firebase/auth'
import { auth } from '../../firebase'
import { verify } from 'crypto'
import VerificationInput from 'react-verification-input'
import { useNavigate } from 'react-router-dom'
var screenshot = require("../../assets/screenshot.jpg");

export default function HomePage() {
    const [phoneNumber, setPhoneNumber] = useState('');
    const [verificationCodeModalOpen, setVerificationCodeModalOpen] = useState(false);
    const navigate = useNavigate();

    function handlePhoneNumberChange(event: React.ChangeEvent<HTMLInputElement>) {
        // Validate that the input is a valid phone number
        const value = event.target.value;

        // If value length greater than 10, or regex does not match, return
        if (value.length > 10 || (value.length > 0 && !/[0-9]/.test(value))) {
            return;
        }

        setPhoneNumber(value);
    }

    async function onSignIn() {
        if (phoneNumber.length == 0) return;

        // @ts-ignore
        signInWithPhoneNumber(auth, "+1" + phoneNumber, window.recaptchaVerifier)
            .then((confirmationResult) => {
                // SMS sent. Prompt user to type the code from the message, then sign the
                // user in with confirmationResult.confirm(code).
                // @ts-ignore
                window.confirmationResult = confirmationResult;

                // Set the verification code modal open
                setVerificationCodeModalOpen(true);
            }).catch((error) => {
                // Error; SMS not sent
                alert(error.message);
            });
    }

    function setUpRecaptcha() {
        // @ts-ignore
        if (window.recaptchaVerifier) {
            return;
        }

        // @ts-ignore
        window.recaptchaVerifier = new RecaptchaVerifier(auth, 'sign-in-button', {
            'size': 'invisible',
            'callback': () => {
                // reCAPTCHA solved, allow signInWithPhoneNumber.
            }
        });

        // @ts-ignore
        window.recaptchaVerifier.verify();
    };

    function checkVerificationCode(code: string) {
        // @ts-ignore
        window.confirmationResult.confirm(code).then(() => {
            // Redirect to Account page
            navigate('/my-account');
        }).catch((error: Error) => {
            // User could not sign in
            alert(error.message);
        });

        // Close the verification code modal
        setVerificationCodeModalOpen(false);
    }

    useEffect(() => {
        setUpRecaptcha();
    }, []);

    return (
        <>
            <div className="bg-white h-screen px-10">
                <div className="relative isolate pt-14">
                    <svg
                        className="absolute inset-0 -z-10 h-full w-full stroke-gray-200 [mask-image:radial-gradient(100%_100%_at_top_right,white,transparent)]"
                        aria-hidden="true"
                    >
                        <defs>
                            <pattern
                                id="83fd4e5a-9d52-42fc-97b6-718e5d7ee527"
                                width={200}
                                height={200}
                                x="50%"
                                y={-1}
                                patternUnits="userSpaceOnUse"
                            >
                                <path d="M100 200V.5M.5 .5H200" fill="none" />
                            </pattern>
                        </defs>
                        <svg x="50%" y={-1} className="overflow-visible fill-gray-50">
                            <path
                                d="M-100.5 0h201v201h-201Z M699.5 0h201v201h-201Z M499.5 400h201v201h-201Z M-300.5 600h201v201h-201Z"
                                strokeWidth={0}
                            />
                        </svg>
                        <rect width="100%" height="100%" strokeWidth={0} fill="url(#83fd4e5a-9d52-42fc-97b6-718e5d7ee527)" />
                    </svg>
                    <div className="mx-auto max-w-7xl px-6 lg:flex lg:items-center lg:gap-x-10 lg:px-8">
                        <div className="mx-auto max-w-2xl lg:mx-0 lg:flex-auto">
                            <h1 className="mt-10 max-w-lg text-4xl font-bold tracking-tight text-gray-900 sm:text-6xl">
                                The daily weather,<br />made easy.
                            </h1>
                            <p className="mt-6 text-lg leading-8 text-gray-600">
                                For decades, we've needed to actively seek out the weather forecast.
                                <br />
                                Now, the weather forecast comes to <b>you</b>.
                            </p>
                            <div className="mt-6 flex items-center gap-x-6">
                                <div className='flex gap-x-6'>
                                    <div className="mt-2 flex rounded-md shadow-sm">
                                        <span className="inline-flex items-center rounded-l-md border border-r-0 border-gray-300 px-3 text-gray-500 sm:text-sm">
                                            +1
                                        </span>
                                        <input
                                            type="tel"
                                            name="phone-num"
                                            id="phone-num"
                                            className="block w-full min-w-0 flex-1 rounded-none rounded-r-md border-0 px-4 py-4 text-gray-900 ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm"
                                            placeholder="Enter your phone number"
                                            value={phoneNumber}
                                            onChange={handlePhoneNumberChange}
                                        />
                                    </div>
                                    <button
                                        type="button"
                                        id="sign-in-button"
                                        className="rounded-md bg-indigo-600 px-3.5 py-2.5 text-sm font-semibold text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600"
                                        onClick={onSignIn}
                                    >
                                        Sign In
                                    </button>
                                </div>
                            </div>
                        </div>
                        <div className="mt-16 sm:mt-24 lg:mt-0 lg:flex-shrink-0 lg:flex-grow">
                            <svg viewBox="0 0 366 729" role="img" className="mx-auto w-[22.875rem] max-w-full drop-shadow-xl">
                                <title>App screenshot</title>
                                <defs>
                                    <clipPath id="2ade4387-9c63-4fc4-b754-10e687a0d332">
                                        <rect width={316} height={684} rx={36} />
                                    </clipPath>
                                </defs>
                                <path
                                    fill="#4B5563"
                                    d="M363.315 64.213C363.315 22.99 341.312 1 300.092 1H66.751C25.53 1 3.528 22.99 3.528 64.213v44.68l-.857.143A2 2 0 0 0 1 111.009v24.611a2 2 0 0 0 1.671 1.973l.95.158a2.26 2.26 0 0 1-.093.236v26.173c.212.1.398.296.541.643l-1.398.233A2 2 0 0 0 1 167.009v47.611a2 2 0 0 0 1.671 1.973l1.368.228c-.139.319-.314.533-.511.653v16.637c.221.104.414.313.56.689l-1.417.236A2 2 0 0 0 1 237.009v47.611a2 2 0 0 0 1.671 1.973l1.347.225c-.135.294-.302.493-.49.607v377.681c0 41.213 22 63.208 63.223 63.208h95.074c.947-.504 2.717-.843 4.745-.843l.141.001h.194l.086-.001 33.704.005c1.849.043 3.442.37 4.323.838h95.074c41.222 0 63.223-21.999 63.223-63.212v-394.63c-.259-.275-.48-.796-.63-1.47l-.011-.133 1.655-.276A2 2 0 0 0 366 266.62v-77.611a2 2 0 0 0-1.671-1.973l-1.712-.285c.148-.839.396-1.491.698-1.811V64.213Z"
                                />
                                <path
                                    fill="#343E4E"
                                    d="M16 59c0-23.748 19.252-43 43-43h246c23.748 0 43 19.252 43 43v615c0 23.196-18.804 42-42 42H58c-23.196 0-42-18.804-42-42V59Z"
                                />
                                <foreignObject
                                    width={316}
                                    height={684}
                                    transform="translate(24 24)"
                                    clipPath="url(#2ade4387-9c63-4fc4-b754-10e687a0d332)"
                                >
                                    <img src={screenshot} alt="" />
                                </foreignObject>
                            </svg>
                        </div>
                    </div>
                </div>
            </div>

            <Transition.Root show={verificationCodeModalOpen} as={Fragment}>
                <Dialog className="relative z-10" onClose={setVerificationCodeModalOpen}>
                    <Transition.Child
                        as={Fragment}
                        enter="ease-out duration-300"
                        enterFrom="opacity-0"
                        enterTo="opacity-100"
                        leave="ease-in duration-200"
                        leaveFrom="opacity-100"
                        leaveTo="opacity-0"
                    >
                        <div className="fixed inset-0 bg-gray-500 bg-opacity-75 transition-opacity" />
                    </Transition.Child>

                    <div className="fixed inset-0 z-10 w-screen overflow-y-auto">
                        <div className="flex min-h-full items-end justify-center p-4 text-center sm:items-center sm:p-0">
                            <Transition.Child
                                as={Fragment}
                                enter="ease-out duration-300"
                                enterFrom="opacity-0 translate-y-4 sm:translate-y-0 sm:scale-95"
                                enterTo="opacity-100 translate-y-0 sm:scale-100"
                                leave="ease-in duration-200"
                                leaveFrom="opacity-100 translate-y-0 sm:scale-100"
                                leaveTo="opacity-0 translate-y-4 sm:translate-y-0 sm:scale-95"
                            >
                                <Dialog.Panel className="relative transform overflow-hidden rounded-lg bg-white px-4 pb-4 pt-5 text-left shadow-xl transition-all sm:my-8 sm:w-full sm:max-w-sm sm:p-6">
                                    <div>
                                        <div className="mt-3 text-center sm:mt-5">
                                            <Dialog.Title as="h3" className="text-base font-semibold leading-6 text-gray-900">
                                                Enter verification code below
                                            </Dialog.Title>
                                            <div className="mt-2">
                                                <p className="text-sm text-gray-500">
                                                    Please enter the code sent to your phone number.
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div className="p-5">
                                        <VerificationInput
                                        classNames={{
                                            container: 'mx-auto',
                                            character: 'rounded-md border border-gray-500 px-3 text-gray-900',
                                        }}
                                        onComplete={checkVerificationCode} />
                                    </div>
                                    <div className="text-xs text-center text-gray-500">
                                        <small><i>By using our application and entering the verification code, you consent to receive text messages from TextTheWeather. Message and data rates may apply.</i></small>
                                    </div>
                                </Dialog.Panel>
                            </Transition.Child>
                        </div>
                    </div>
                </Dialog>
            </Transition.Root>
        </>
    )
}