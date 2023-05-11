# Example of a Azure Queue Trigger data provider to Occtoo

The program contains two functions. One http triggered that will add three the messages to the queue, call this one from a browser when running localy . The second one will trigger on any messages added and converts each message into a Dynamic Entity. They are then imported into Occtoo using the [OnboardingServiceClient](https://github.com/Occtoo/Occtoo.Onboarding.Sdk)


Check our documentation on [how to set up a Provider in Occtoo Studio](https://docs.occtoo.com/docs/get-started/provide-data#12-create-data-provider) to get your provider details.



